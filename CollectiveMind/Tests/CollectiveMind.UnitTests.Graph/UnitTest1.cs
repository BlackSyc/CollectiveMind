using System;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Repositories;
using Neo4j.Driver;
using Newtonsoft.Json;
using Xunit;

namespace CollectiveMind.UnitTests.Graph
{
	public class UnitTest1
	{
		[Fact]
		public async Task CreateSingleNode()
		{
			var driver =
				GraphDatabase.Driver(new Uri("bolt://localhost:1002"), AuthTokens.Basic("neo4j", "wachtwoord"));
			var statementRepo = new StatementRepository(driver);

			var newStatement = new Statement
			{
				Title = "This is my first statement",
				Body = "And this is its body."
			};
			
			var statement = await statementRepo.CreateAsync(newStatement);

			Assert.NotNull(statement);
			Assert.True(Equal(newStatement, statement));
		}

		[Fact]
		public async Task GetSingleNode()
		{
			var driver =
				GraphDatabase.Driver(new Uri("bolt://localhost:1002"), AuthTokens.Basic("neo4j", "wachtwoord"));
			var statementRepo = new StatementRepository(driver);

			var newStatement = await statementRepo.CreateAsync(new Statement
			{
				Title = "I want to test",
				Body = "if I can fetch this again."
			});

			var statement = await statementRepo.GetOrDefaultAsync<Statement>(newStatement.Id, CancellationToken.None);

			Assert.NotNull(statement);
			Assert.True(Equal(newStatement, statement));
		}

		public bool Equal<T>(T expected, T actual)
		{
			var expectedJson = JsonConvert.SerializeObject(expected);
			var actualJson = JsonConvert.SerializeObject(actual);

			return expectedJson == actualJson;
		}
	}
}