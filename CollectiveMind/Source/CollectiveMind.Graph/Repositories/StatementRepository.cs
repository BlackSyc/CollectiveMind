using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;
using Neo4j.Driver;

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