using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Exceptions;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Statements
{
	/// <inheritdoc />
	public class StatementService : IStatementService
	{
		private readonly IStatementRepository _statementRepository;

		/// <summary>
		/// Default constructor for creating a new instance of <see cref="StatementService"/>.
		/// </summary>
		/// <param name="statementRepository">A repository to handle storage-related functionality.</param>
		public StatementService(IStatementRepository statementRepository)
		{
			_statementRepository = statementRepository;
		}

		/// <inheritdoc />
		public async Task<Statement> GetStatementByIdAsync(Guid statementId, CancellationToken cancellationToken = default)
		{
			Guard.Against.Default(statementId, nameof(statementId));
			
			var statement = await _statementRepository.GetByIdOrDefaultAsync(statementId, cancellationToken);

			if (statement == default)
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}

			return statement;
		}

		/// <inheritdoc />
		public async Task<Statement> CreateStatementAsync(StatementParameters newStatement)
		{
			Guard.Against.Null(newStatement, nameof(newStatement));

			var statement = await _statementRepository.CreateAsync(new Statement
			{
				Title = newStatement.Title,
				Body = newStatement.Body
			});

			return statement;
		}

		/// <inheritdoc />
		public Task<IEnumerable<Statement>> SearchByTitleAsync(string searchFilter, int skip, int limit, CancellationToken cancellationToken = default)
		{
			Guard.Against.NullOrEmpty(searchFilter, nameof(searchFilter));

			return _statementRepository.SearchByTitleAsync(searchFilter, skip, limit, cancellationToken);
		}
	}
}