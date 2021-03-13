﻿using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace CollectiveMind.Graph.Repositories
{
	public class NodeRepository : INodeRepository
	{
		protected readonly IAsyncSession GraphSession;

		public NodeRepository(IDriver graphDriver)
		{
			GraphSession = graphDriver.AsyncSession();
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
			return JsonConvert.DeserializeObject<T>(newNodeJson);
		}
		
		public async Task<T> GetOrDefaultAsync<T>(Guid identifier, CancellationToken cancellationToken = default)
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
			return JsonConvert.DeserializeObject<T>(newNodeJson);
		}

		public Task<bool> ExistsAsync<T>(Guid nodeId, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		protected static string ToCustomJson(object node)
		{
			var nodeJson = JsonConvert.SerializeObject(node);
			
			string regexPattern = "\"([^\"]+)\":"; // the "propertyName": pattern
			
			return Regex.Replace(nodeJson, regexPattern, "$1:");
		}
	}
}