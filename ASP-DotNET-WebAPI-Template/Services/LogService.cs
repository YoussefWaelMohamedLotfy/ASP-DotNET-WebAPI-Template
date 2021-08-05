using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.Models;
using ASP_DotNET_WebAPI_Template.Repositories.Interfaces;
using X.PagedList;

namespace ASP_DotNET_WebAPI_Template.Services
{
    public class LogService
    {
        private readonly IUnitOfWork _unit;

        public LogService(IUnitOfWork unit)
        {
            _unit = unit ?? throw new System.ArgumentNullException(nameof(unit));
        }

        public Task<IPagedList<Log>> GetPaginatedLogs(RequestParams param)
            => _unit.Logs.GetPagedList(param);
    }
}