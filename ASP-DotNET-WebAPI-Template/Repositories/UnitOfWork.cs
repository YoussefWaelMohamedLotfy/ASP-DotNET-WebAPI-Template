using System;
using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.DbContexts;
using ASP_DotNET_WebAPI_Template.Models;
using ASP_DotNET_WebAPI_Template.Repositories.Interfaces;

namespace ASP_DotNET_WebAPI_Template.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IGenericRepository<Log> _logs { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public IGenericRepository<Log> Logs => _logs ?? new GenericRepository<Log>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}