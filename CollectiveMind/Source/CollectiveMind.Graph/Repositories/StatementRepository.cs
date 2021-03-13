using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace CollectiveMind.Graph.Repositories
{
	public class StatementRepository : NodeRepository, IStatementRepository
	{
		public StatementRepository(IDriver graphDriver) : base(graphDriver)
		{
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

			var firstNodeDict = result["firstNode"].As<INode>().Properties;
			var secondNodeDict = result["secondNode"].As<INode>().Properties;


			var newFirstNodeJson = JsonConvert.SerializeObject(firstNodeDict);
			var newFirstNode = JsonConvert.DeserializeObject<Statement>(newFirstNodeJson);
			
			var newSecondNodeJson = JsonConvert.SerializeObject(secondNodeDict);
			var newSecondNode = JsonConvert.DeserializeObject<Statement>(newSecondNodeJson);

			return (newFirstNode, newSecondNode);
		}

		public Task<Statement> GetByIdOrDefaultAsync(Guid statementId, CancellationToken cancellationToken)
		{
			return GetOrDefaultAsync<Statement>(statementId, cancellationToken);
		}

		public Task<Statement> CreateRelatedStatementAsync<TRelation>(Guid statementId, Statement newArgument)
			where TRelation : Relation
		{
			return base.CreateRelatedNodeAsync<TRelation, Statement>(statementId, newArgument);
		}

		public Task<bool> ExistsAsync(Guid nodeId, CancellationToken cancellationToken = default)
		{
			return ExistsAsync<Statement>(nodeId, cancellationToken);
		}

		public Task<Statement> CreateAsync(Statement newStatement)
		{
			return base.CreateAsync(newStatement);
		}

		public Task<IEnumerable<Statement>> GetRelatedStatements<TRelation>(Guid statementId, CancellationToken cancellationToken = default) where TRelation : Relation
		{
			return base.GetRelatedNodesAsync<TRelation, Statement>(statementId, cancellationToken);
		}

		public Task<Statement> LinkExistingStatements<TRelation>(Guid statementId, Guid argumentId) where TRelation : Relation
		{
			return base.LinkExistingNodesAsync<TRelation, Statement>(statementId, argumentId);
		}
	}
}