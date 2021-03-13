using System;
using System.Threading.Tasks;
using CollectiveMind.Business.Services;
using CollectiveMind.Graph.Entities.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StatementController : ControllerBase
	{
		private readonly IStatementService _statementService;

		public StatementController(IStatementService statementService)
		{
			_statementService = statementService;
		}

		[HttpGet("{statementId}")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await _statementService.GetStatementByIdAsync(statementId, HttpContext.RequestAborted));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Statement statement)
		{
			return Ok(await _statementService.CreateStatementAsync(statement));
		}
	}
}