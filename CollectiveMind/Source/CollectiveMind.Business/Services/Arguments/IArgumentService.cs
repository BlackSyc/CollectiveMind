using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Business.Models;
using CollectiveMind.Graph.Nodes;

namespace CollectiveMind.Business.Services.Arguments
{
	public interface IArgumentService
	{
		Task<Statement> CreateArgumentForAsync(Guid statementId, Statement argument);

		Task<IEnumerable<Statement>> GetArgumentsForAsync(Guid statementId, CancellationToken cancellationToken = default);

		Task<Statement> LinkExistingArgumentAsync(Guid statementId, Guid argumentId);
		
		
	}
}