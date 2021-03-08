using System;
using System.Threading;
using System.Threading.Tasks;
using CollectiveMind.Data.DataContext;
using CollectiveMind.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CollectiveMind.Data.Repositories
{
	public class StatementRepository : IStatementRepository
	{
		private readonly CollectiveMindContext _context;

		public StatementRepository(CollectiveMindContext context)
		{
			_context = context;
		}

		public async Task<Statement> AddStatementAsync(Statement statement)
		{
			return (await _context.Set<Statement>().AddAsync(statement)).Entity;
		}

		public Task<Statement> GetStatementAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return _context.Set<Statement>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}
	}
}