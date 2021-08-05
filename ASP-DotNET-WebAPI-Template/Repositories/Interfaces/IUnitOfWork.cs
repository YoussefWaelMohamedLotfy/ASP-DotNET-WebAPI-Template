using System;
using System.Threading.Tasks;

namespace ASP_DotNET_WebAPI_Template.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}