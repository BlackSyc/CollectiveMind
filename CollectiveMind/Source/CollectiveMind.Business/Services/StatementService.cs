using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Exceptions;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services
{
	public class StatementService : IStatementService
	{
		private readonly IStatementRepository _statementRepository;

		public StatementService(IStatementRepository statementRepository)
		{
			_statementRepository = statementRepository;
		}

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

		public async Task<Statement> CreateStatementAsync(Statement newStatement)
		{
			Guard.Against.Null(newStatement, nameof(newStatement));

			var statement = await _statementRepository.CreateAsync(newStatement);

			return statement;
		}
	}
}