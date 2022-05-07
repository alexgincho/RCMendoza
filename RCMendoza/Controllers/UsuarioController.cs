using Microsoft.AspNetCore.Mvc;
using RCMendoza.Models.Interfaces;
using RCMendoza.Models.Services;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCMendoza.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioService IUsuario;
        private IRolesService _sRol;
        private IDocumentosService _sDoc;
        public UsuarioController(IUsuarioService _IUsuario, IRolesService sRol, IDocumentosService sDoc)
        {
            IUsuario = _IUsuario;
            this._sRol = sRol;
            this._sDoc = sDoc;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MantenimientoUsuario()
        {
            Usuario entity = null;
            ViewBag.Roles = _sRol.GetRoles();
            ViewBag.Doc = _sDoc.GetDocumento();
            return PartialView("_MantenimientoUsuario",entity ?? new Usuario());
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MantenimientoUsuario([FromBody] Usuario usuario)
        {
            Result result = null;
            try
            {
                result = new Result();
                if (ModelState.IsValid)
                {
                    if (usuario.IdUsuario != 0)
                    {
                        result = await IUsuario.UpdateUsuario(usuario);
                        if (!result.Success) { throw new Exception(result.Message); }
                    }
                    else
                    {
                        result = await IUsuario.CreateUsuario(usuario);
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
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await IUsuario.DeleteUsuario(id);
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
        public async Task<IActionResult> GetUsuario(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await IUsuario.Get(id);
                if (!result.Success) { throw new Exception(result.Message); }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            Result result = null;
            try
            {
                result = new Result();
                result = await IUsuario.GetAll();
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
