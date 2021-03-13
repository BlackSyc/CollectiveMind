using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Exceptions;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Arguments
{
	public abstract class ArgumentService<T> : IArgumentService where T : Relation
	{
		private readonly IStatementRepository _statementRepository;

		protected ArgumentService(IStatementRepository statementRepository)
		{
			_statementRepository = statementRepository;
		}

		public async Task<Statement> CreateArgumentForAsync(Guid statementId, Statement newArgument)
		{
			Guard.Against.Default(statementId, nameof(statementId));
			Guard.Against.Null(newArgument, nameof(newArgument));
			
			if (!await _statementRepository.ExistsAsync(statementId))
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}
			
			var argument = await _statementRepository
				.CreateRelatedStatementAsync<T>(statementId, newArgument);
			return argument;
		}

		public async Task<IEnumerable<Statement>> GetArgumentsForAsync(Guid statementId, CancellationToken cancellationToken = default)
		{
			Guard.Against.Default(statementId, nameof(statementId));

			if (!await _statementRepository.ExistsAsync(statementId, cancellationToken))
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}
			
			return await _statementRepository.GetRelatedStatements<T>(statementId, cancellationToken);
		}

		public async Task<Statement> LinkExistingArgumentAsync(Guid statementId, Guid argumentId)
		{
			Guard.Against.Default(statementId, nameof(statementId));
			Guard.Against.Default(argumentId, nameof(argumentId));
			
			if (!await _statementRepository.ExistsAsync(statementId))
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}
			
			if (!await _statementRepository.ExistsAsync(argumentId))
			{
				throw new EntityNotFoundException(argumentId, typeof(Statement));
			}

			return await _statementRepository.LinkExistingStatements<T>(statementId, argumentId);
		}
	}
}