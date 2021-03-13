using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectiveMind.Business.Services.Arguments;
using CollectiveMind.Graph.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers
{
	[ApiController]
	public class PositiveController : ControllerBase
	{
		private readonly IPositiveArgumentService _positiveArgumentService;

		public PositiveController(IPositiveArgumentService positiveArgumentService)
		{
			_positiveArgumentService = positiveArgumentService;
		}


		/// <summary>
		/// Retrieves a list of all positive arguments for the statement with the specified identifier.
		/// </summary>
		/// <param name="statementId"></param>
		/// <returns></returns>
		[HttpGet("/Statement/{statementId}/Positive")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await _positiveArgumentService.GetArgumentsForAsync(statementId, HttpContext.RequestAborted));
		}

		[HttpPost("/Statement/{statementId}/Positive")]
		public async Task<IActionResult> Post([FromRoute] Guid statementId, [FromBody] Statement statement)
		{
			return Ok(await _positiveArgumentService.CreateArgumentForAsync(statementId, statement));
		}
	}
}