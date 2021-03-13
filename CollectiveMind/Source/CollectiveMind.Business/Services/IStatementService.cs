using System;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;

namespace CollectiveMind.Business.Services
{
	public interface IStatementService
	{
		Task<Statement> GetStatementByIdAsync(Guid statementId, CancellationToken cancellationToken = default);

		Task<Statement> CreateStatementAsync(Statement newStatement);

		Task<Statement> UpdateStatementAsync(Guid statementId, Statement statement);

		Task<Statement> DeleteStatementAsync(Guid statementId);
	}
}