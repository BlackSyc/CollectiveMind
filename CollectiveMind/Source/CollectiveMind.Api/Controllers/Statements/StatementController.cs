using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectiveMind.Business.Services.Statements;
using CollectiveMind.Graph.Entities.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers.Statements
{
	/// <summary>
	/// Controller that defines all direct <see cref="Statement"/>-related operations.
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class StatementController : ControllerBase
	{
		private readonly IStatementService _statementService;

		/// <summary>
		/// Default constructor for creating a new instance of <see cref="StatementController"/>.
		/// </summary>
		/// <param name="statementService">An implementation of <see cref="IStatementService"/> that
		/// is used to perform <see cref="Statement"/>-related business logic.</param>
		public StatementController(IStatementService statementService)
		{
			_statementService = statementService;
		}

		/// <summary>
		/// Retrieves a <see cref="Statement"/> by a specified identifier.
		/// </summary>
		/// <param name="statementId">The identifier of the <see cref="Statement"/> that will be retrieved.</param>
		/// <returns>A <see cref="Statement"/> with matching specified identifier.</returns>
		[ProducesResponseType(typeof(Statement), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet("{statementId}")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await _statementService.GetStatementByIdAsync(statementId, HttpContext.RequestAborted));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchFilter"></param>
		/// <param name="skip"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(IEnumerable<Statement>), StatusCodes.Status200OK)]
		[HttpGet("/Search")]
		public async Task<IActionResult> Search([FromQuery] string searchFilter, [FromQuery] int skip = 0, int limit = 10)
		{
			return Ok(await _statementService.SearchByTitleAsync(searchFilter, skip, limit, HttpContext.RequestAborted));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="statement"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(Statement), StatusCodes.Status201Created)]		
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Statement statement)
		{
			return Created(Request.GetEncodedUrl(), await _statementService.CreateStatementAsync(statement));
		}
	}
}