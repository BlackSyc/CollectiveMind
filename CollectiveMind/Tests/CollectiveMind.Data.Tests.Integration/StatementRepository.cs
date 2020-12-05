using System.Collections.Generic;
using CollectiveMind.Data.Models;
using CollectiveMind.Data.Repositories;
using Xunit;

namespace CollectiveMind.Data.Tests.Integration
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			// Arrange
			var statementRepository = CreateRepository();
			
			// Act
			statementRepository.AddStatementAsync(CreateStatementTree());
			
			// Assert
		}
		
		

		private static IStatementRepository CreateRepository()
		{
			return new StatementRepository();
		}
		
		private static Statement CreateStatementTree()
		{
			return new Statement
			{
				Value = new StatementValue("There have been humans on the moon."),
				PositiveArguments = new List<Statement>
				{
					new Statement
					{
						Value = new StatementValueWithExternalSource(
							"We have the following video proof.", "www.youtube.com/proofvideo"),
						PositiveArguments = new List<Statement>
						{
							new Statement
							{
								Value = new StatementValue("That video is staged, because the flag is moving but there is no wind."),
								NegativeArguments = new List<Statement>
								{
									new Statement
									{
										Value = new StatementValue("There is no drag, so the flag is still moving from the motion of putting it there."),
									}
								}
							}
						}
					}
				}
			};
		}
	}
}