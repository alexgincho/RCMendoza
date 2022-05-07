using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System.Threading.Tasks;

namespace RCMendoza.Models.Interfaces
{
    public interface IUsuarioService
    {
        public Task<Result> CreateUsuario(Usuario usuario);
        public Task<Result> UpdateUsuario(Usuario usuario);
        public Task<Result> DeleteUsuario(int id);
        public Task<Result> GetAll();
        public Usuario Get(int id);
    }
}
