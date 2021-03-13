using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Exceptions;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace CollectiveMind.Graph.Repositories
{
	/// <inheritdoc />
	public class NodeRepository : INodeRepository
	{
		/// <summary>
		/// The graph session used for communication with the graph.
		/// </summary>
		protected readonly IAsyncSession GraphSession;

		/// <summary>
		/// Default constructor for creating a new instance of <see cref="NodeRepository"/>.
		/// </summary>
		/// <param name="graphDriver">The graph driver used for communication with the graph.</param>
		protected NodeRepository(IDriver graphDriver)
		{
			GraphSession = graphDriver.AsyncSession();
		}
		
		/// <inheritdoc />
		public async Task<TNode> CreateAsync<TNode>(TNode node) 
			where TNode : Node
		{
			if (node.Id != default)
			{
				throw new InvalidIdentifierException(node.Id);
			}

			node.Id = Guid.NewGuid();
			var nodeJson = ToCustomJson(node);

			var query = $"CREATE (newNode:{typeof(TNode).Name} {nodeJson}) RETURN newNode";
			
			var result = await GraphSession
				.WriteTransactionAsync(async tx =>
				{
					var res = await tx.RunAsync(query);
					if (await res.FetchAsync())
					{
						return res.Current;
					}
					return null;
				});

			var firstLevel = result["newNode"].As<INode>().Properties;

			var newNodeJson = JsonConvert.SerializeObject(firstLevel);
			return JsonConvert.DeserializeObject<TNode>(newNodeJson);
		}
		
		/// <inheritdoc />
		public async Task<TNode> GetOrDefaultAsync<TNode>(Guid identifier, CancellationToken cancellationToken = default)
			where TNode : Node
		{
			var query = "MATCH (existingNode {Id: '" + identifier + "'}) RETURN existingNode";
			var result = await GraphSession.ReadTransactionAsync(async tx =>
			{
				var res = tx.RunAsync(query);
				if (await res.Result.FetchAsync())
				{
					return res.Result.Current;
				}
				return null;
			});
			
			var firstLevel = result["existingNode"].As<INode>().Properties;

			var newNodeJson = JsonConvert.SerializeObject(firstLevel);
			return JsonConvert.DeserializeObject<TNode>(newNodeJson);
		}

		/// <inheritdoc />
		public async Task<bool> ExistsAsync<TNode>(Guid nodeId, CancellationToken cancellationToken = default)
			where TNode : Node
		{
			var query = $"MATCH (existingNode: {typeof(TNode).Name}) WHERE existingNode.Id = '{nodeId}' RETURN existingNode";

			var result = await GraphSession.ReadTransactionAsync(async tx =>
			{
				var res = tx.RunAsync(query);
				if (await res.Result.FetchAsync())
				{
					return res.Result.Current;
				}

				return null;
			});

			return result?.Keys?.Contains("existingNode") ?? false;
		}
		
		/// <inheritdoc />
		public async Task<IEnumerable<TNode>> GetRelatedNodesAsync<TRelation, TNode>(Guid originNodeId, 
			CancellationToken cancellationToken = default) 
			where TRelation : Relation
			where TNode : Node
		{
			var query = $"MATCH (n {{Id: '{originNodeId}'}})-[:{typeof(TRelation).Name}]->(relatedNode:{typeof(TNode).Name}) "
			            + "RETURN relatedNode";
			
			var result = await GraphSession.ReadTransactionAsync(async tx => 
				await (await tx.RunAsync(query)).ToListAsync());

			return result.Where(x => x.Keys.Contains("relatedNode"))
				.Select(x => x["relatedNode"].As<INode>())
				.Select(x => x.Properties)
				.Select(JsonConvert.SerializeObject)
				.Select(JsonConvert.DeserializeObject<TNode>);
		}

		/// <inheritdoc />
		public async Task<TLinkedNode> LinkExistingNodesAsync<TRelation, TLinkedNode>(Guid originNodeId, Guid linkedNodeId) 
			where TRelation : Relation 
			where TLinkedNode : Node
		{
			var query =
				$"MATCH (originNode), (linkedNode:{typeof(TLinkedNode).Name}) " +
				$"WHERE originNode.Id = '{originNodeId}' AND linkedNode.Id = '{linkedNodeId}' " +
				$"CREATE (originNode)-[r:{typeof(TRelation).Name}]->(linkedNode) " +
				"RETURN linkedNode";

			var result = await GraphSession.WriteTransactionAsync(async tx =>
			{
				var res = tx.RunAsync(query);

				if (await res.Result.FetchAsync())
				{
					return res.Result.Current;
				}

				return null;
			});

			var linkedNodeProperties = result["linkedNode"].As<INode>().Properties;

			var linkedNodeJson = JsonConvert.SerializeObject(linkedNodeProperties);
			return JsonConvert.DeserializeObject<TLinkedNode>(linkedNodeJson);
		}

		/// <inheritdoc />
		public async Task<TNode> CreateRelatedNodeAsync<TRelation, TNode>(Guid originNodeId, TNode newNode) 
			where TRelation : Relation
			where TNode : Node
		{
			if (newNode.Id != default)
			{
				throw new InvalidIdentifierException(newNode.Id);
			}

			newNode.Id = Guid.NewGuid();
			var nodeJson = ToCustomJson(newNode);

			var query =
				"MATCH (originNode) " +
				$"WHERE originNode.Id = '{originNodeId}' " +
				"WITH originNode " + 
				$"CREATE (originNode)-[r:{typeof(TRelation).Name}]->(newNode:{typeof(TNode).Name} {nodeJson}) " +
				"RETURN newNode";
			
			var result = await GraphSession.WriteTransactionAsync(async tx =>
			{
				var res = tx.RunAsync(query);

				if (await res.Result.FetchAsync())
				{
					return res.Result.Current;
				}

				return null;
			});

			var relatedNodeProperties = result["newNode"].As<INode>().Properties;

			var relatedNodeJson = JsonConvert.SerializeObject(relatedNodeProperties);
			return JsonConvert.DeserializeObject<TNode>(relatedNodeJson);
		}
		
		/// <summary>
		/// Converts a class to graph-specific JSON needed in a CYPHER-create query.
		/// </summary>
		/// <param name="node">The node that will be converted.</param>
		/// <returns>A string containing the converted JSON.</returns>
		protected static string ToCustomJson(object node)
		{
			const string regexPattern = "\"([^\"]+)\":"; // the "propertyName": pattern
			
			var nodeJson = JsonConvert.SerializeObject(node);

			return Regex.Replace(nodeJson, regexPattern, "$1:");
		}
	}
}