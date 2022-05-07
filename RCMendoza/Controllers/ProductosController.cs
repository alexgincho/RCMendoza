using Microsoft.AspNetCore.Mvc;
using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System;
using System.Threading.Tasks;

namespace RCMendoza.Controllers
{
    public class ProductosController : Controller
    {
        private IProductoService IProducto;
        public ProductosController(IProductoService _IProducto)
        {
            IProducto = _IProducto;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProductosAll()
        {
            Respuesta rpta = new Respuesta();
            try
            {
                var LstProducto = IProducto.GetAll();
                if (true)
                {
                    rpta.Data = LstProducto;
                    rpta.State = 200;
                    rpta.Message = "Success";
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.State = 400;
                rpta.Message = "Error";
            }
            return Ok(rpta);
        }

        [HttpPost]
        public async Task<IActionResult> MantenimientoProducto([FromBody] Producto producto)
        {
            Result result = null;
            try
            {
                result = new Result();
                if (ModelState.IsValid)
                {
                    if (producto.IdProducto != 0)
                    {
                        result = await IProducto.UpdateProducto(producto);
                        if (!result.Success) { throw new Exception(result.Message); }
                    }
                    else
                    {
                        result = await IProducto.CreateProducto(producto);
                        if (!result.Success) { throw new Exception(result.Message); }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = null;
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await IProducto.DeleteProducto(id);
                if (!result.Success)
                {
                    throw new Exception(result.Message);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducto(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await IProducto.Get(id);
                if (!result.Success) { throw new Exception(result.Message); }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await IProducto.GetAll();
                if (!result.Success)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Ok(result);
        }
    }
}
