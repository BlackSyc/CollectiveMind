using System;
using System.Threading.Tasks;
using CollectiveMind.Graph.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StatementController : ControllerBase
	{
		private readonly IStatementNodeRepository _statementNodeRepository;

		public StatementController(IStatementNodeRepository statementNodeRepository)
		{
			_statementNodeRepository = statementNodeRepository;
		}
		
		[HttpGet("{statementId}")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Graph.Nodes.Statement statement)
		{
			return Ok(await _statementNodeRepository.CreateAsync(statement));
		}
		
		[HttpPut]
		public async Task<IActionResult> Put([FromQuery] Guid statementId, [FromBody] Graph.Nodes.Statement statement)
		{
			return Ok();
		}
		
		[HttpDelete]
		public async Task<IActionResult> Delete([FromQuery] Guid statementId)
		{
			return Ok();
		}
	}
}