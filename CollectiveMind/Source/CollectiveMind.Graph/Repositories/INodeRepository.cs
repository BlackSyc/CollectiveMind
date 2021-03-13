using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;

namespace CollectiveMind.Graph.Repositories
{
	public interface INodeRepository
	{
		Task<TNode> CreateAsync<TNode>(TNode node) 
			where TNode : Node;

		Task<TNode> GetOrDefaultAsync<TNode>(Guid identifier, CancellationToken cancellationToken = default)
			where TNode : Node;
		
		Task<bool> ExistsAsync<TNode>(Guid nodeId, CancellationToken cancellationToken = default) 
			where TNode : Node;

		Task<IEnumerable<TNode>> GetRelatedNodesAsync<TRelation, TNode>(Guid statementId, CancellationToken cancellationToken = default) 
			where TRelation : Relation 
			where TNode : Node;
	}
}