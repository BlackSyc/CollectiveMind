using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;

namespace CollectiveMind.Graph.Repositories
{
	public interface IStatementRepository
	{
		Task<Statement> GetByIdOrDefaultAsync(Guid statementId, CancellationToken cancellationToken);
		Task<Statement> CreateRelatedStatementAsync<TRelation>(Guid statementId, Statement newArgument) 
			where TRelation : Relation;

		Task<bool> ExistsAsync(Guid nodeId, CancellationToken cancellationToken = default);

		Task<Statement> CreateAsync(Statement newStatement);
		
		Task<IEnumerable<Statement>> GetRelatedStatements<T>(Guid statementId, CancellationToken cancellationToken = default) 
			where T : Relation;
		Task<Statement> LinkExistingStatements<T>(Guid statementId, Guid argumentId) 
			where T : Relation;
	}
}