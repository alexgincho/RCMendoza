using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;

namespace RCMendoza.Controllers
{
    public class AdminProveedorController : Controller
    {
        private readonly IProveedorService _IProveedor;
        public AdminProveedorController(IProveedorService prove)
        {
            _IProveedor = prove;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MantenimientoProveedor([FromBody]Proveedor proveedor)
        {
            Result result = null;
            try
            {
                result = new Result();
                if (ModelState.IsValid)
                {
                    if(proveedor.IdProveedor != 0)
                    {
                        // Update
                    }
                    else
                    {
                        // Create
                        result = await _IProveedor.CreateProveedor(proveedor, result);
                        if (!result.Success) { throw new Exception(); }
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
    }
}
