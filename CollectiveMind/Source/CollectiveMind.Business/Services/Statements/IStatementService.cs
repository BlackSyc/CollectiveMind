using System;
using System.Threading;
using System.Threading.Tasks;
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
		/// Creates a new statement.
		/// </summary>
		/// <param name="newStatement">The statement that will be created.</param>
		/// <returns>The newly created statement.</returns>
		Task<Statement> CreateStatementAsync(Statement newStatement);
	}
}