using System;
using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;
using Neo4j.Driver;

namespace CollectiveMind.Graph.Repositories
{
	public class StatementNodeRepository : IStatementNodeRepository
	{
		private readonly IDriver _graphDriver;

		public StatementNodeRepository(IDriver graphDriver)
		{
			_graphDriver = graphDriver;
		}

		public async Task<Statement> SaveAsync(Statement statement)
		{
			if (!string.IsNullOrWhiteSpace(statement.Id))
			{
				throw new Exception($"You're not allowed to update a statement node.");
			}

			statement.Id = Guid.NewGuid().ToString();
			
			var session = _graphDriver.AsyncSession();

			var result = await session.WriteTransactionAsync(async tx 
				=>
			{
				var res = await tx.RunAsync("CREATE (a:Statement $statement) RETURN a", new {statement});

				if (await res.FetchAsync())
				{
					return res.Current;
				}

				return null;
			});

			return statement;
		}
	}
}