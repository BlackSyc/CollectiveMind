using System;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Data.Models;

namespace CollectiveMind.Data.Repositories
{
	public interface IStatementRepository
	{
		Task<Statement> AddStatementAsync(Statement statement);

		Task<Statement> GetStatementAsync(Guid id, CancellationToken cancellationToken = default);
	}
}