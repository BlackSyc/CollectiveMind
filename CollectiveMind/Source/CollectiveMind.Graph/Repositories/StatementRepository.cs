using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace CollectiveMind.Graph.Repositories
{
	/// <inheritdoc cref="IStatementRepository"/>
	public class StatementRepository : NodeRepository, IStatementRepository
	{
		/// <summary>
		/// Default constructor for creating a new instance of <see cref="StatementRepository"/>.
		/// </summary>
		/// <param name="graphDriver">The graph driver used to communicate with the graph.</param>
		public StatementRepository(IDriver graphDriver) : base(graphDriver)
		{
		}

		public async Task<IEnumerable<Statement>> SearchByTitleAsync(string searchFilter, int skip, int limit, CancellationToken cancellationToken = default)
		{

			var query =
				$"MATCH(s:Statement) WITH s, size(apoc.coll.intersection(split(s.Title, ' '), {JsonConvert.SerializeObject(searchFilter.Split(" "))})) AS c WHERE c > 0 RETURN s ORDER BY c DESC SKIP {skip} LIMIT {limit}";

			var result = await GraphSession.ReadTransactionAsync(async tx => 
				await (await tx.RunAsync(query)).ToListAsync());

			return result
				.Where(x => x.Keys.Contains("s"))
				.Select(x => x["s"].As<INode>())
				.Select(x => x.Properties)
				.Select(JsonConvert.SerializeObject)
				.Select(JsonConvert.DeserializeObject<Statement>);
		}
		
		/// <inheritdoc />
		public Task<Statement> GetByIdOrDefaultAsync(Guid statementId, CancellationToken cancellationToken)
		{
			return GetOrDefaultAsync<Statement>(statementId, cancellationToken);
		}

		/// <inheritdoc />
		public Task<Statement> CreateRelatedStatementAsync<TRelation>(Guid statementId, Statement newArgument)
			where TRelation : Relation
		{
			return CreateRelatedNodeAsync<TRelation, Statement>(statementId, newArgument);
		}

		/// <inheritdoc />
		public Task<bool> ExistsAsync(Guid statementId, CancellationToken cancellationToken = default)
		{
			return ExistsAsync<Statement>(statementId, cancellationToken);
		}

		/// <inheritdoc />
		public Task<Statement> CreateAsync(Statement newStatement)
		{
			return CreateAsync<Statement>(newStatement);
		}

		/// <inheritdoc />
		public Task<IEnumerable<Statement>> GetRelatedStatements<TRelation>(Guid originStatementId, CancellationToken cancellationToken = default) where TRelation : Relation
		{
			return GetRelatedNodesAsync<TRelation, Statement>(originStatementId, cancellationToken);
		}

		/// <inheritdoc />
		public Task<Statement> LinkExistingStatements<TRelation>(Guid statementId, Guid argumentId) where TRelation : Relation
		{
			return LinkExistingNodesAsync<TRelation, Statement>(statementId, argumentId);
		}
	}
}