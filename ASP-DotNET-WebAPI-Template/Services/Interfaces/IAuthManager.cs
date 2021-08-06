using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.DTOs;

namespace ASP_DotNET_WebAPI_Template.Services.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO loginUserDTO);

        Task<string> CreateToken();
    }
}