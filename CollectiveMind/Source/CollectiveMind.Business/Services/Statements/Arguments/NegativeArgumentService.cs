using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Statements.Arguments
{
	/// <inheritdoc cref="INegativeArgumentService"/>
	public class NegativeArgumentService : ArgumentService<NegativeArgument>, INegativeArgumentService
	{
		/// <summary>
		/// Default constructor for creating a new instance of <see cref="NegativeArgumentService"/>.
		/// </summary>
		/// <param name="statementRepository">The repository used to handle storage related logic.</param>
		public NegativeArgumentService(IStatementRepository statementRepository) : base(statementRepository) { }
	}
}