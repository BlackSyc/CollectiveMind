using System.Threading.Tasks;
using CollectiveMind.Data.Models;

namespace CollectiveMind.Data.Repositories
{
	public interface IStatementRepository
	{
		Task<Statement> AddStatementAsync(Statement statement);

		Task<Statement> FindStatementByValue(StatementValue statementValue);
	}
}