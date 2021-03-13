using System;
using System.Threading.Tasks;
using CollectiveMind.Business.Services.Statements.Arguments;
using CollectiveMind.Graph.Entities.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace CollectiveMind.Controllers.Statements.Relations
{
	/// <summary>
	/// Controller that defines all negative argument <see cref="Statement"/>-related operations.
	/// </summary>
	[ApiController]
	public class NegativeController : ControllerBase
	{
		private readonly INegativeArgumentService _negativeArgumentService;

		/// <summary>
		/// Default constructor for creating a new instance of <see cref="NegativeController"/>.
		/// </summary>
		/// <param name="negativeArgumentService">A service to handle business-related logic.</param>
		public NegativeController(INegativeArgumentService negativeArgumentService)
		{
			_negativeArgumentService = negativeArgumentService;
		}

		/// <summary>
		/// Retrieves a list of all negative arguments for the statement with the specified identifier.
		/// </summary>
		/// <param name="statementId">The identifier of the statement for which all negative arguments will be
		/// retrieved.</param>
		/// <returns>A list of all negative arguments for the statement with the specified identifier.</returns>
		[HttpGet("/Statement/{statementId}/Negative")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await _negativeArgumentService.GetArgumentsForAsync(statementId, HttpContext.RequestAborted));
		}

		/// <summary>
		/// Creates a new statement as a negative argument for another existing statement.
		/// </summary>
		/// <param name="statementId">The identifier of the existing statement for which the negative argument will
		/// be created.</param>
		/// <param name="statement">The statement that will be created as a negative argument for the existing statement.</param>
		/// <returns>The newly created negative argument statement.</returns>
		[HttpPost("/Statement/{statementId}/Negative")]
		public async Task<IActionResult> Create([FromRoute] Guid statementId, [FromBody] Statement statement)
		{
			return Ok(await _negativeArgumentService.CreateArgumentForAsync(statementId, statement));
		}

		/// <summary>
		/// Links two existing statements together by setting the second as a negative argument to the first.
		/// </summary>
		/// <param name="statementId">The identifier of the statement the negative argument will be linked to.</param>
		/// <param name="argumentId">The identifier of the statement that will be used as a negative argument.</param>
		/// <returns>The updated statement that is now a negative argument to the other statement.</returns>
		[HttpPost("/Statement/{statementId}/Negative/{argumentId}")]
		public async Task<IActionResult> Link([FromRoute] Guid statementId, [FromRoute] Guid argumentId)
		{
			return Ok(await _negativeArgumentService.LinkExistingArgumentAsync(statementId, argumentId));
		}
	}
}