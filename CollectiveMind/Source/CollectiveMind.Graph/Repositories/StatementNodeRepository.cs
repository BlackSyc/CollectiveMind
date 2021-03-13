using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace CollectiveMind.Graph.Repositories
{
	public class StatementNodeRepository : IStatementNodeRepository
	{
		private readonly IAsyncSession _graphSession;

		public StatementNodeRepository(IDriver graphDriver)
		{
			_graphSession = graphDriver.AsyncSession();
		}

		public async Task<(Statement, Statement)> CreateConnectedStatementAsync(Statement firstStatement, Statement secondStatement, string relationType)
		{
			if (firstStatement.Id != default || secondStatement.Id != default)
			{
				throw new Exception("Could not create node: The Id must be empty.");
			}

			firstStatement.Id = Guid.NewGuid();
			secondStatement.Id = Guid.NewGuid();
			
			var firstStatementJson = ToCustomJson(firstStatement);
			var secondStatementJson = ToCustomJson(secondStatement);

			var query = $"CREATE (firstNode:{nameof(Statement)} {firstStatementJson})-[:{relationType}]->(secondNode:{nameof(Statement)} {secondStatementJson}) RETURN *";

			var result = await _graphSession
				.WriteTransactionAsync(async tx =>
				{
					var res = await tx.RunAsync(query);
					if (await res.FetchAsync())
					{
						return res.Current;
					}
					return null;
				});

			var firstNodeDict = result["firstNode"].As<INode>().Properties;
			var secondNodeDict = result["secondNode"].As<INode>().Properties;


			var newFirstNodeJson = JsonConvert.SerializeObject(firstNodeDict);
			var newFirstNode = JsonConvert.DeserializeObject<Statement>(newFirstNodeJson);
			
			var newSecondNodeJson = JsonConvert.SerializeObject(secondNodeDict);
			var newSecondNode = JsonConvert.DeserializeObject<Statement>(newSecondNodeJson);

			return (newFirstNode, newSecondNode);
		}

		public async Task<T> CreateAsync<T>(T node) where T : Node
		{
			if (node.Id != default)
			{
				throw new Exception("Could not create node: The Id must be empty.");
			}

			node.Id = Guid.NewGuid();
			var nodeJson = ToCustomJson(node);

			var query = $"CREATE (newNode:{typeof(T).Name} {nodeJson}) RETURN newNode";
			
			var result = await _graphSession
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
			return JsonConvert.DeserializeObject<T>(newNodeJson);
		}

		public async Task<T> GetOrDefaultAsync<T>(Guid identifier, CancellationToken cancellationToken)
		{
			var query = "MATCH (existingNode {Id: '" + identifier + "'}) RETURN existingNode";
			var result = await _graphSession.ReadTransactionAsync(async tx =>
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
			return JsonConvert.DeserializeObject<T>(newNodeJson);
		}

		private static string ToCustomJson(object node)
		{
			var nodeJson = JsonConvert.SerializeObject(node);
			
			string regexPattern = "\"([^\"]+)\":"; // the "propertyName": pattern
			
			return Regex.Replace(nodeJson, regexPattern, "$1:");
		}
		
	}
}