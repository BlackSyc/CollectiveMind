using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Exceptions;

namespace CollectiveMind.Business.Services.Statements
{
	/// <summary>
	/// An implementation of this interface handles <see cref="Statement"/>-related business logic.
	/// </summary>
	public interface IStatementService
	{
		/// <summary>
		/// Retrieves an existing <see cref="Statement"/> with the specified identifier.
		/// </summary>
		/// <param name="statementId">The identifier of the existing statement.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <returns>The statement with the specified identifier.</returns>
		/// <exception cref="EntityNotFoundException">When no statement with the specified identifier
		/// was found.</exception>
		Task<Statement> GetStatementByIdAsync(Guid statementId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a new statement using the specified statement parameters.
		/// </summary>
		/// <param name="newStatement">The statement parameters for the statement that will be created.</param>
		/// <returns>The newly created statement.</returns>
		Task<Statement> CreateStatementAsync(StatementParameters newStatement);

		/// <summary>
		/// Searches through all statements and returns all statements that have
		/// a title that matches the search filter.
		/// </summary>
		/// <param name="searchFilter">The search filter used to search by title.</param>
		/// <param name="skip">Pagination parameter indicating how many results should be skipped and
		/// therefore not returned.</param>
		/// <param name="limit">Pagination parameter indicating the maximum number of results
		/// that should be returned.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <returns>A list of statements matching the search filter parameter.</returns>
		Task<IEnumerable<Statement>> SearchByTitleAsync(string searchFilter, int skip, int limit,
			CancellationToken cancellationToken = default);
	}
}