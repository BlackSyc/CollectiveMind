using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectiveMind.Business.Models;
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
		/// Searches through all statements using a search filter and returns matching statements ordered by matching
		/// keywords.
		/// </summary>
		/// <param name="searchFilter">Contains keywords that will be used to find matching statements.</param>
		/// <param name="skip">Paging parameter specifying the number of results in the ordered list that will be
		/// skipped.</param>
		/// <param name="limit">Paging parameter specifying the number of statements that may be returned.</param>
		/// <returns>A list of matching statements ordered by number of distinct matching keywords.</returns>
		[ProducesResponseType(typeof(IEnumerable<Statement>), StatusCodes.Status200OK)]
		[HttpGet("/Search")]
		public async Task<IActionResult> Search([FromQuery] string searchFilter, [FromQuery] int skip = 0, int limit = 10)
		{
			return Ok(await _statementService.SearchByTitleAsync(searchFilter, skip, limit, HttpContext.RequestAborted));
		}

		/// <summary>
		/// Creates a new statement using the specified statement parameters.
		/// </summary>
		/// <param name="statementParameters">The parameters from which the new statement will be created.</param>
		/// <returns>The newly created statement.</returns>
		[ProducesResponseType(typeof(Statement), StatusCodes.Status201Created)]		
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] StatementParameters statementParameters)
		{
			return Created(Request.GetEncodedUrl(), await _statementService.CreateStatementAsync(statementParameters));
		}
	}
}