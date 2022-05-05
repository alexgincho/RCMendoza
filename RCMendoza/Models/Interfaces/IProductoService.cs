using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System.Threading.Tasks;

namespace RCMendoza.Models.Interfaces
{
    public interface IProductoService
    {
        public Task<Result> CreateProducto(Producto producto);
        public Task<Result> UpdateProducto(Producto producto);
        public Task<Result> DeleteProducto(int id);
        public Task<Result> GetAll();
        public Task<Result> Get(int id);
    }
}
