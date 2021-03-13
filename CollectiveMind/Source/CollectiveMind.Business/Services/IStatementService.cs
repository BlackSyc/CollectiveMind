using System;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;

namespace CollectiveMind.Business.Services
{
	public interface IStatementService
	{
		Task<Statement> GetStatementByIdAsync(Guid statementId, CancellationToken cancellationToken = default);

		Task<Statement> CreateStatementAsync(Statement newStatement);
	}
}