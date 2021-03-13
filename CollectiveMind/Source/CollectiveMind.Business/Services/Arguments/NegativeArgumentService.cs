using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Arguments
{
	public class NegativeArgumentService : ArgumentService, INegativeArgumentService
	{
		protected override ArgumentType ArgumentType => ArgumentType.Negative;

		public NegativeArgumentService(IStatementNodeRepository statementNodeRepository) : base(statementNodeRepository)
		{
		}
	}
}