﻿using System;
using System.Threading.Tasks;
using CollectiveMind.Business.Services.Statements;
using CollectiveMind.Graph.Entities.Nodes;
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
		[HttpGet("{statementId}")]
		public async Task<IActionResult> Get([FromRoute] Guid statementId)
		{
			return Ok(await _statementService.GetStatementByIdAsync(statementId, HttpContext.RequestAborted));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="statement"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Statement statement)
		{
			return Ok(await _statementService.CreateStatementAsync(statement));
		}
	}
}