using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;

namespace CollectiveMind.Graph.Repositories
{
	public interface IStatementNodeRepository
	{
		Task<T> CreateAsync<T>(T node) where T : Node;

		Task<(Statement, Statement)> CreateConnectedStatementAsync(Statement firstStatement, Statement secondStatement,
			string relationType);
	}
}