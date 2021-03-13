using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectiveMind.Graph.Nodes;
using CollectiveMind.Graph.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers
{
	[ApiController]
	public class PositiveController : ControllerBase
	{
		private readonly IStatementNodeRepository _statementNodeRepository;

		public PositiveController(IStatementNodeRepository statementNodeRepository)
		{
			_statementNodeRepository = statementNodeRepository;
		}

		/// <summary>
		/// Retrieves a list of all positive arguments for the statement with the specified identifier.
		/// </summary>
		/// <param name="statementId"></param>
		/// <returns></returns>
		[HttpGet("/Statement/{statementId}/Positive")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await Task.FromResult(new List<Statement>()));
		}

		[HttpPost("/Statement/{statementId}/Positive")]
		public async Task<IActionResult> Post([FromRoute] Guid statementId, [FromBody] Statement statement)
		{
			return Ok(await _statementNodeRepository.CreateAsync(statement));
		}
		
		[HttpDelete("/Statement/{originStatementId}/Positive/{statementId}")]
		public async Task<IActionResult> Delete([FromRoute] Guid originStatementId, [FromRoute] Guid statementId)
		{
			return Ok();
		}
		
	}
}