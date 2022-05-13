using System.Threading.Tasks;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;

namespace RCMendoza.Models.Interfaces
{
    public interface IProveedorService
    {
        public Task<Result> CreateProveedor(Proveedor proveedor );
    }
}
