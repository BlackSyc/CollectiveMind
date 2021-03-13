using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectiveMind.Graph.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CollectiveMind.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		private readonly IStatementNodeRepository _statementNodeRepository;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IStatementNodeRepository statementNodeRepository)
		{
			_logger = logger;
			_statementNodeRepository = statementNodeRepository;
		}

		
		[HttpPost]
		public async Task<IActionResult> CreateStatement([FromBody] Graph.Nodes.Statement statement)
		{
			return Ok(await _statementNodeRepository.CreateAsync(statement));
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
				{
					Date = DateTime.Now.AddDays(index),
					TemperatureC = rng.Next(-20, 55),
					Summary = Summaries[rng.Next(Summaries.Length)]
				})
				.ToArray();
		}
	}
}