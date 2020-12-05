using System;

namespace CollectiveMind.Data.Models
{
	public class StatementValue
	{
		public Guid Id { get; }
		
		public string Text { get; }

		public StatementValue(string text)
		{
			Text = text;
		}
	}
}