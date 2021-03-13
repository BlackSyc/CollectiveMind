using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Arguments
{
	public class NegativeArgumentService : ArgumentService<NegativeArgument>, INegativeArgumentService
	{
		public NegativeArgumentService(IStatementRepository statementRepository) : base(statementRepository)
		{
		}
	}
}