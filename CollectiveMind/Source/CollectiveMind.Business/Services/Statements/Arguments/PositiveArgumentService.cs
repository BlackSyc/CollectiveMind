using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Statements.Arguments
{
	/// <inheritdoc cref="IPositiveArgumentService"/>
	public class PositiveArgumentService : ArgumentService<PositiveArgument>, IPositiveArgumentService
	{
		/// <summary>
		/// Default constructor for creating a new instance of <see cref="PositiveArgumentService"/>.
		/// </summary>
		/// <param name="statementRepository">The repository used to handle storage related logic.</param>
		public PositiveArgumentService(IStatementRepository statementRepository) : base(statementRepository) { }
	}
}