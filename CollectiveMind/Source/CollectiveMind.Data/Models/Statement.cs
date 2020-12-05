using System;
using System.Collections.Generic;

namespace CollectiveMind.Data.Models
{
	/// <summary>
	/// Represents a simple statement that could either be true or false.
	/// </summary>
	public class Statement
	{
		public Guid Id { get; set; }
		
		public StatementValue Value { get; set;}
		
		public List<Statement> NegativeArguments { get; set; }
		
		public List<Statement> PositiveArguments { get; set; }
		
		public List<Statement> NegativeArgumentFor { get; set; }
		
		public List<Statement> PositiveArgumentFor { get; set; }
	}
}