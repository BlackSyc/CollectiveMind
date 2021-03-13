using System;
using System.Threading.Tasks;
using CollectiveMind.Business.Services.Arguments;
using CollectiveMind.Graph.Entities.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers
{
	[ApiController]
	public class NegativeController : ControllerBase
	{
		private readonly INegativeArgumentService _negativeArgumentService;

		public NegativeController(INegativeArgumentService negativeArgumentService)
		{
			_negativeArgumentService = negativeArgumentService;
		}

		/// <summary>
		/// Retrieves a list of all negative arguments for the statement with the specified identifier.
		/// </summary>
		/// <param name="statementId"></param>
		/// <returns></returns>
		[HttpGet("/Statement/{statementId}/Negative")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await _negativeArgumentService.GetArgumentsForAsync(statementId, HttpContext.RequestAborted));
		}

		[HttpPost("/Statement/{statementId}/Negative")]
		public async Task<IActionResult> Post([FromRoute] Guid statementId, [FromBody] Statement statement)
		{
			return Ok(await _negativeArgumentService.CreateArgumentForAsync(statementId, statement));
		}

		[HttpPost("/Statement/{statementId}/Negative/{argumentId}")]
		public async Task<IActionResult> Create([FromRoute] Guid statementId, [FromRoute] Guid argumentId)
		{
			return Ok(await _negativeArgumentService.LinkExistingArgumentAsync(statementId, argumentId));
		}
	}
}