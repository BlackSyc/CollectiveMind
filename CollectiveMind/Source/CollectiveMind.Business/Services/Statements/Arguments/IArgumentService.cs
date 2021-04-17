using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Exceptions;

namespace CollectiveMind.Business.Services.Statements.Arguments
{
	/// <summary>
	/// An implementation of this service contains business logic related to <see cref="Statement"/>s related to others
	/// by definition of an argument.
	/// </summary>
	public interface IArgumentService
	{
		/// <summary>
		/// Creates a new <see cref="Statement"/> as an argument for an existing statement with the specified identifier.
		/// </summary>
		/// <param name="statementId">The identifier of the statement for which an argument will be created.</param>
		/// <param name="newArgumentParameters">The parameters for the statement that will be created as
		/// an argument.</param>
		/// <returns>The newly created statement.</returns>
		/// <exception cref="EntityNotFoundException">When no existing statement with the specified identifier
		/// was found. The argument is not created.</exception>
		Task<Statement> CreateArgumentForAsync(Guid statementId, StatementParameters newArgumentParameters);

		/// <summary>
		/// Retrieves all arguments for a statement with a specified identifier.
		/// </summary>
		/// <param name="statementId">The identifier of the statement for which arguments will be retrieved.</param>
		/// <param name="cancellationToken">A cancellation token used to cancel the request.</param>
		/// <returns>An enumerable of <see cref="Statement"/>s that were used as an argument for the statement
		/// with the specified identifier.</returns>
		/// <exception cref="EntityNotFoundException">When no existing statement with the specified identifier
		/// was found.</exception>
		Task<IEnumerable<Statement>> GetArgumentsForAsync(Guid statementId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Links two existing arguments with a relation.
		/// </summary>
		/// <param name="statementId">The identifier of the statement to which the argument will be linked.</param>
		/// <param name="argumentId">The identifier of the argument that will be linked to the statement.</param>
		/// <returns>The updated argument.</returns>
		/// <exception cref="EntityNotFoundException">When no existing statement or argument statement with the
		/// specified identifier was found. No links were created.</exception>
		Task<Statement> LinkExistingArgumentAsync(Guid statementId, Guid argumentId);
	}
}