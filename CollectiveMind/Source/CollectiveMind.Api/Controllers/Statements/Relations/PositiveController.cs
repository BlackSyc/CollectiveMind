using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectiveMind.Business.Models;
using CollectiveMind.Business.Services.Statements.Arguments;
using CollectiveMind.Graph.Entities.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers.Statements.Relations
{
	/// <summary>
	/// Controller that defines all positive argument <see cref="Statement"/>-related operations.
	/// </summary>
	[ApiController]
	public class PositiveController : ControllerBase
	{
		private readonly IPositiveArgumentService _positiveArgumentService;

		/// <summary>
		/// Default constructor for creating a new instance of <see cref="PositiveController"/>.
		/// </summary>
		/// <param name="positiveArgumentService">A service to handle business-related logic.</param>
		public PositiveController(IPositiveArgumentService positiveArgumentService)
		{
			_positiveArgumentService = positiveArgumentService;
		}

		/// <summary>
		/// Retrieves a list of all positive arguments for the statement with the specified identifier.
		/// </summary>
		/// <param name="statementId">The identifier of the statement for which all positive arguments will be
		/// retrieved.</param>
		/// <returns>A list of all positive arguments for the statement with the specified identifier.</returns>
		[ProducesResponseType(typeof(IEnumerable<Statement>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet("/Statement/{statementId}/Positive")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await _positiveArgumentService.GetArgumentsForAsync(statementId, HttpContext.RequestAborted));
		}

		/// <summary>
		/// Creates a new statement as a positive argument for another existing statement.
		/// </summary>
		/// <param name="statementId">The identifier of the existing statement for which the positive argument will
		/// be created.</param>
		/// <param name="statementParameters">The parameters for the statement that will be created as a
		/// positive argument for the existing statement.</param>
		/// <returns>The newly created positive argument statement.</returns>
		[ProducesResponseType(typeof(Statement), StatusCodes.Status201Created)]		
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpPost("/Statement/{statementId}/Positive")]
		public async Task<IActionResult> Create([FromRoute] Guid statementId, [FromBody] StatementParameters statementParameters)
		{
			return Ok(await _positiveArgumentService.CreateArgumentForAsync(statementId, statementParameters));
		}

		/// <summary>
		/// Links two existing statements together by setting the second as a positive argument to the first.
		/// </summary>
		/// <param name="statementId">The identifier of the statement the positive argument will be linked to.</param>
		/// <param name="argumentId">The identifier of the statement that will be used as a positive argument.</param>
		/// <returns>The updated statement that is now a positive argument to the other statement.</returns>
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpPost("/Statement/{statementId}/Positive/{argumentId}")]
		public async Task<IActionResult> Link([FromRoute] Guid statementId, [FromRoute] Guid argumentId)
		{
			return Created(Request.GetEncodedUrl(), await _positiveArgumentService.LinkExistingArgumentAsync(statementId, argumentId));
		}
	}
}