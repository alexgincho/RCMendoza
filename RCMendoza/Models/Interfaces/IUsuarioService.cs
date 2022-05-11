using RCMendoza.Models.ModelDB;
using RCMendoza.Request;
using RCMendoza.Response;
using System.Threading.Tasks;

namespace RCMendoza.Models.Interfaces
{
    public interface IUsuarioService
    {
        public Task<Result> CreateUsuario(UsuarioDto usuario);
        public Task<Result> UpdateUsuario(UsuarioDto usuario);
        public Task<Result> DeleteUsuario(int id);
        public Task<Result> GetAll();
        public Usuario Get(int id);
    }
}
