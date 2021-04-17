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
	/// Represents a statement node repository for read and write access to a statement graph.
	/// </summary>
	public interface IStatementRepository
	{
		/// <summary>
		/// Retrieves an existing statement from the graph with the specified identifier or default
		/// if no match is found.
		/// </summary>
		/// <param name="statementId">The identifier of the statement that will be retrieved.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <returns>The statement with matching identifier, or default if no match was found.</returns>
		Task<Statement> GetByIdOrDefaultAsync(Guid statementId, CancellationToken cancellationToken);
		
		/// <summary>
		/// Creates a new statement as an argument to another existing statement with the specified identifier..
		/// </summary>
		/// <param name="statementId">The identifier of the existing statement the argument will be created for.</param>
		/// <param name="newArgument">The new statement that will be created as an argument.</param>
		/// <typeparam name="TRelation">The type of argument as will be specified in the relation between
		/// the statements.</typeparam>
		/// <returns>The newly created statement.</returns>
		/// <exception cref="InvalidIdentifierException">When the statement that was to be created contains an
		/// identifier that is already set.</exception>
		Task<Statement> CreateRelatedStatementAsync<TRelation>(Guid statementId, Statement newArgument) 
			where TRelation : Relation;

		/// <summary>
		/// Checks whether a statement with the specified identifier exists on the graph.
		/// </summary>
		/// <param name="statementId">The identifier that will be looked for the graph.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <returns>Whether or not a statement with matching identifier could be found on the graph. True if it was
		/// found, false otherwise.</returns>
		Task<bool> ExistsAsync(Guid statementId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new statement with no relations on the graph.
		/// </summary>
		/// <param name="newStatement">The statement that will be created.</param>
		/// <returns>The newly created statement as present on the graph.</returns>
		/// <exception cref="InvalidIdentifierException">When the statement that was to be created contains an
		/// identifier that is already set.</exception>
		Task<Statement> CreateAsync(Statement newStatement);
		
		/// <summary>
		/// Retrieves all statements that are related through a specified relation type
		/// to an origin statement specified by its identifier.
		/// </summary>
		/// <param name="originStatementId">The identifier of the origin statement that will be checked for statements
		/// through the specified relation type.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <typeparam name="T">The type of relation that will be checked for.</typeparam>
		/// <returns>An enumerable of all statements that were connected to the origin statement with the specified
		/// relation type.</returns>
		Task<IEnumerable<Statement>> GetRelatedStatements<T>(Guid originStatementId, CancellationToken cancellationToken = default) 
			where T : Relation;
		
		/// <summary>
		/// Links two existing statements to each other with the a new relation of the specified type.
		/// </summary>
		/// <param name="statementId">The identifier of the origin statement from which a link will be created.</param>
		/// <param name="argumentId">The identifier of the statement to which a link will be created.</param>
		/// <typeparam name="T">The type of relation that defines the link created between the statements.</typeparam>
		/// <returns>The updated linked statement as present on the graph.</returns>
		Task<Statement> LinkExistingStatements<T>(Guid statementId, Guid argumentId) 
			where T : Relation;

		/// <summary>
		/// Searches through all statements and returns a page (depending on the pagination parameters) of all
		/// statements that have a title that matches the provided search filter.
		/// </summary>
		/// <param name="searchFilter">The search filter used to search for matching titles.</param>
		/// <param name="skip">Pagination parameter that indicates how many results should be skipped and
		/// therefore not returned.</param>
		/// <param name="limit">Pagination parameter that indicates the maximum amount of results that
		/// should be returned.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <returns>A page of statements that match the provided search filter.</returns>
		Task<IEnumerable<Statement>> SearchByTitleAsync(string searchFilter, int skip, int limit,
			CancellationToken cancellationToken = default);
	}
}