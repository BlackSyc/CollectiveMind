using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Exceptions;
using CollectiveMind.Graph.Nodes;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Arguments
{
	public abstract class ArgumentService : IArgumentService
	{
		protected abstract ArgumentType ArgumentType { get; }

		private readonly IStatementNodeRepository _statementNodeRepository;

		protected ArgumentService(IStatementNodeRepository statementNodeRepository)
		{
			_statementNodeRepository = statementNodeRepository;
		}

		public async Task<Statement> CreateArgumentForAsync(Guid statementId, Statement newArgument)
		{
			Guard.Against.Default(statementId, nameof(statementId));
			Guard.Against.Null(newArgument, nameof(newArgument));
			
			if (!await _statementNodeRepository.ExistsAsync(statementId))
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}
			
			var argument = await _statementNodeRepository
				.CreateRelatedStatementAsync(statementId, newArgument, ArgumentType.ToString());
			return argument;
		}

		public Task<IEnumerable<Statement>> GetArgumentsForAsync(Guid statementId, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<Statement> LinkExistingArgumentAsync(Guid statementId, Guid argumentId)
		{
			throw new NotImplementedException();
		}
	}
}