using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RCMendoza.Models.Interfaces;
using RCMendoza.Request;
using RCMendoza.Response;
using System;
using System.Threading.Tasks;

namespace RCMendoza.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _TokenService;
        private readonly ILoginService _ILogin;
        private string generarToken = null;

        public LoginController(IConfiguration config, ITokenService token, ILoginService Ilogin)
        {
            _config = config;   
            _TokenService = token;  
            _ILogin = Ilogin;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticacion([FromBody]Login oModel)
        {
            Result result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _ILogin.AuthLogin(oModel);
                    if (usuario!= null)
                    {
                        result = new Result();
                        generarToken = _TokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(),usuario);
                        if(generarToken != null)
                        {
                            HttpContext.Session.SetString("token", generarToken);
                            result.Success = true;
                            result.Message = "Ok";
                            result.Data = new { Token = generarToken };
                        }
                    }
                    else { throw new Exception(); }
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
