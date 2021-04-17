using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Entities.Nodes;
using CollectiveMind.Graph.Entities.Relations;
using CollectiveMind.Graph.Exceptions;
using CollectiveMind.Graph.Repositories;

namespace CollectiveMind.Business.Services.Statements.Arguments
{
	/// <summary>
	/// Represents an abstract implementation of business logic related to arguments of type <see cref="Statement"/>
	/// belonging to another statement.
	/// </summary>
	/// <typeparam name="T">The type of argument this service contains business logic for.</typeparam>
	public abstract class ArgumentService<T> : IArgumentService where T : Relation
	{
		private readonly IStatementRepository _statementRepository;

		/// <summary>
		/// Constructor for instantiating base class fields.
		/// </summary>
		/// <param name="statementRepository">An implementation of <see cref="IStatementRepository"/> to delegate
		/// <see cref="Statement"/>-related storage functionality.</param>
		protected ArgumentService(IStatementRepository statementRepository)
		{
			_statementRepository = statementRepository;
		}

		/// <inheritdoc />
		public async Task<Statement> CreateArgumentForAsync(Guid statementId, StatementParameters newArgumentParameters)
		{
			Guard.Against.Default(statementId, nameof(statementId));
			Guard.Against.Null(newArgumentParameters, nameof(newArgumentParameters));

			if (!await _statementRepository.ExistsAsync(statementId))
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}

			var argument = await _statementRepository.CreateRelatedStatementAsync<T>(
				statementId, 
				new Statement
				{
					Title = newArgumentParameters.Title,
					Body = newArgumentParameters.Body
				});
			return argument;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Statement>> GetArgumentsForAsync(Guid statementId,
			CancellationToken cancellationToken = default)
		{
			Guard.Against.Default(statementId, nameof(statementId));

			if (!await _statementRepository.ExistsAsync(statementId, cancellationToken))
			{
				throw new EntityNotFoundException(statementId, typeof(Statement));
			}

			return await _statementRepository.GetRelatedStatements<T>(statementId, cancellationToken);
		}

		/// <inheritdoc />
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