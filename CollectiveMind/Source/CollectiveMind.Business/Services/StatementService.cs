using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CollectiveMind.Graph.Exceptions;
using CollectiveMind.Graph.Nodes;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services
{
	public class StatementService : IStatementService
	{
		private readonly IStatementNodeRepository _statementNodeRepository;

		public StatementService(IStatementNodeRepository statementNodeRepository)
		{
			_statementNodeRepository = statementNodeRepository;
		}

		public async Task<Statement> GetStatementByIdAsync(Guid statementId, CancellationToken cancellationToken = default)
		{
			Guard.Against.Default(statementId, nameof(statementId));
			
			var statement = await _statementNodeRepository.GetByIdOrDefaultAsync(statementId, cancellationToken);

			if (statement == default)
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}

			return statement;
		}

		public async Task<Statement> CreateStatementAsync(Statement newStatement)
		{
			Guard.Against.Null(newStatement, nameof(newStatement));

			var statement = await _statementNodeRepository.CreateAsync(newStatement);

			return statement;
		}

		public Task<Statement> UpdateStatementAsync(Guid statementId, Statement statement)
		{
			throw new NotImplementedException();
		}

		public Task<Statement> DeleteStatementAsync(Guid statementId)
		{
			throw new NotImplementedException();
		}
	}
}