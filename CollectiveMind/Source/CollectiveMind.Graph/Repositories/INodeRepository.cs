using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Exceptions;

namespace CollectiveMind.Graph.Repositories
{
	/// <summary>
	/// Represents a basic node repository for access to a generic node graph.
	/// </summary>
	public interface INodeRepository
	{
		/// <summary>
		/// Creates a new node of type <typeparam name="TNode"></typeparam> without any relations defined on the graph.
		/// </summary>
		/// <param name="node">The new node that will be created on the graph.</param>
		/// <typeparam name="TNode">The type of the node that will be created.</typeparam>
		/// <returns>The newly created node as it's now specified in the graph.</returns>
		/// <exception cref="InvalidIdentifierException">When the node that was to be created contains an identifier
		/// that is already set.</exception>
		Task<TNode> CreateAsync<TNode>(TNode node) 
			where TNode : Node;

		/// <summary>
		/// Retrieves an existing node with the specified identifier or default if no match was found.
		/// </summary>
		/// <param name="identifier">The identifier of the existing node that will be retrieved.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <typeparam name="TNode">The type of the node that will be retrieved.</typeparam>
		/// <returns>The node with the specified identifier, or default if no match was found.</returns>
		Task<TNode> GetOrDefaultAsync<TNode>(Guid identifier, CancellationToken cancellationToken = default)
			where TNode : Node;
		
		/// <summary>
		/// Checks whether a node exists on the graph with the specified identifier.
		/// </summary>
		/// <param name="nodeId">The identifier that will be checked for presence in the graph.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <typeparam name="TNode">The type of the node that will be checked for existence.</typeparam>
		/// <returns>Whether or not a node of specified type with specified identifier exists. True if it exists,
		/// false otherwise.</returns>
		Task<bool> ExistsAsync<TNode>(Guid nodeId, CancellationToken cancellationToken = default) 
			where TNode : Node;

		/// <summary>
		/// Retrieves all nodes related to an origin node by the specified relation type.
		/// </summary>
		/// <param name="originNodeId">The identifier of the origin node whose relations will be checked.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <typeparam name="TRelation">The type of relation that should be looked for.</typeparam>
		/// <typeparam name="TNode">The type of node that should be looked for.</typeparam>
		/// <returns>An enumerable of all nodes of the specified type that are connected to the origin node through
		/// a relation with the specified type.</returns>
		Task<IEnumerable<TNode>> GetRelatedNodesAsync<TRelation, TNode>(Guid originNodeId, CancellationToken cancellationToken = default) 
			where TRelation : Relation 
			where TNode : Node;

		/// <summary>
		/// Links two existing nodes together with a relation of the specified type.
		/// </summary>
		/// <param name="originNodeId">The identifier of the origin node that will be linked to the linked node.</param>
		/// <param name="linkedNodeId">The identifier of the linked node that will be linked tot the origin
		/// node.</param>
		/// <typeparam name="TRelation">The type of relation that will be made between the two nodes.</typeparam>
		/// <typeparam name="TLinkedNode">The type of the linked node.</typeparam>
		/// <returns>The updated linked node as stored in the graph.</returns>
		Task<TLinkedNode> LinkExistingNodesAsync<TRelation, TLinkedNode>(Guid originNodeId, Guid linkedNodeId)
			where TRelation : Relation
			where TLinkedNode : Node;

		/// <summary>
		/// Creates a new node and a relation to it from an origin node with the specified identifier.
		/// </summary>
		/// <param name="originNodeId">The identifier of the origin node from which a relation will be made
		/// to the newly created node.</param>
		/// <param name="newNode">The node that will be created.</param>
		/// <typeparam name="TRelation">The type of relation that will be made from the origin node
		/// to the new node.</typeparam>
		/// <typeparam name="TNode">The type of the new node.</typeparam>
		/// <returns>The newly created node as stored in the graph.</returns>
		/// <exception cref="InvalidIdentifierException">When the node that was to be created contains an identifier
		/// that is already set.</exception>
		Task<TNode> CreateRelatedNodeAsync<TRelation, TNode>(Guid originNodeId, TNode newNode)
			where TRelation : Relation
			where TNode : Node;
	}
}