using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System.Threading.Tasks;

namespace RCMendoza.Models.Interfaces
{
    public interface IClienteService
    {
        public Task<Result> CreateCliente(Usuario Cliente);
        public Task<Result> UpdateCliente(Usuario Cliente);
        public Task<Result> DeleteCliente(int id);
        public Task<Result> GetAll();
        public Task<Result> Get(int id);
    }
}
