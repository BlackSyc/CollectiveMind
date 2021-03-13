using System;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;

namespace CollectiveMind.Graph.Repositories
{
	public interface INodeRepository
	{
		Task<T> CreateAsync<T>(T node) where T : Node;

		Task<T> GetOrDefaultAsync<T>(Guid identifier, CancellationToken cancellationToken = default);
		
		Task<bool> ExistsAsync<T>(Guid nodeId, CancellationToken cancellationToken = default);
	}
}