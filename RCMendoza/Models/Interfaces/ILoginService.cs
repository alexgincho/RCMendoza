using RCMendoza.Models.ModelDB;
using RCMendoza.Request;
using RCMendoza.Response;
using System.Threading.Tasks;

namespace RCMendoza.Models.Interfaces
{
    public interface ILoginService
    {
        public Task<Usuario> AuthLogin(Login oModel);
    }
}
