using Microsoft.AspNetCore.Mvc;
using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System;
using System.Threading.Tasks;

namespace RCMendoza.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteService ICliente;
        public ClienteController(IClienteService _ICliente)
        {
            ICliente = _ICliente;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MantenimientoCliente()
        {
            Usuario entity = null;
            return PartialView("_MantenimientoCliente", entity ?? new Usuario());
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MantenimientoCliente([FromBody] Usuario cliente)
        {
            Result result = null;
            try
            {
                result = new Result();
                if (ModelState.IsValid)
                {
                    if (cliente.IdUsuario != 0)
                    {
                        result = await ICliente.UpdateCliente(cliente);
                        if (!result.Success) { throw new Exception(result.Message); }
                    }
                    else
                    {
                        result = await ICliente.CreateCliente(cliente);
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
        public async Task<IActionResult> DeleteCliente(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await ICliente.DeleteCliente(id);
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
        public async Task<IActionResult> GetCliente(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await ICliente.Get(id);
                if (!result.Success) { throw new Exception(result.Message); }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCliente()
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await ICliente.GetAll();
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
