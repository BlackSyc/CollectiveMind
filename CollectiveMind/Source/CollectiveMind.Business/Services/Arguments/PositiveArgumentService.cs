using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Arguments
{
	public class PositiveArgumentService : ArgumentService, IPositiveArgumentService
	{
		protected override ArgumentType ArgumentType => ArgumentType.Positive;

		public PositiveArgumentService(IStatementNodeRepository statementNodeRepository) : base(statementNodeRepository)
		{
		}
	}
}