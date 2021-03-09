using System;
using System.Threading.Tasks;
using Neo4j.Driver;
using Xunit;

namespace CollectiveMind.UnitTests.Graph
{
	public class UnitTest1
	{
		[Fact]
		public async Task Test1()
		{
			var driver = GraphDatabase.Driver(new Uri("bolt://localhost:1002"), AuthTokens.Basic("neo4j", "wachtwoord"));

			var session = driver.AsyncSession();
			
			// var result = await session.WriteTransactionAsync(async tx 
			// 	=> await tx.RunAsync("CREATE (a:Animal $animal)", new {animal = new Animal
			// 	{
			// 		Name = "Barry",
			// 		Age = 12,
			// 	}}));
		}
	}
}