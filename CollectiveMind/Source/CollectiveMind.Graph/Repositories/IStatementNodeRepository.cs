using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;

namespace CollectiveMind.Graph.Repositories
{
	public interface IStatementNodeRepository
	{
		Task<Statement> SaveAsync(Statement statement);
	}
}