using System.Threading.Tasks;
using CollectiveMind.Data.Models;

namespace CollectiveMind.Data.Repositories
{
	public class StatementRepository : IStatementRepository
	{
		public Task<Statement> AddStatementAsync(Statement statement)
		{
			throw new System.NotImplementedException();
		}

		public Task<Statement> FindStatementByValue(StatementValue statementValue)
		{
			throw new System.NotImplementedException();
		}
	}
}