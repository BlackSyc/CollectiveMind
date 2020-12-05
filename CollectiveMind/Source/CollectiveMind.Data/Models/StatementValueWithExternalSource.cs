namespace CollectiveMind.Data.Models
{
	public class StatementValueWithExternalSource : StatementValue
	{
		public string UrlToExternalSource { get; }
		
		public StatementValueWithExternalSource(string text, string urlToExternalSource) : base(text)
		{
			UrlToExternalSource = urlToExternalSource;
		}
	}
}