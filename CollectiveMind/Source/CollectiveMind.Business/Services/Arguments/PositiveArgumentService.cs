using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Arguments
{
	public class PositiveArgumentService : ArgumentService<PositiveArgument>, IPositiveArgumentService
	{
		public PositiveArgumentService(IStatementRepository statementRepository) : base(statementRepository)
		{
		}
	}
}