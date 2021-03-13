using System;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;

namespace CollectiveMind.Graph.Repositories
{
	public interface IStatementNodeRepository
	{
		Task<(Statement, Statement)> CreateConnectedStatementAsync(Statement firstStatement, Statement secondStatement,
			string relationType);

		Task<Statement> GetByIdOrDefaultAsync(Guid statementId, CancellationToken cancellationToken);
		Task<Statement> CreateRelatedStatementAsync(Guid statementId, Statement newArgument, string relationName);

		Task<bool> ExistsAsync(Guid nodeId, CancellationToken cancellationToken = default);

		Task<Statement> CreateAsync(Statement newStatement);
	}
}