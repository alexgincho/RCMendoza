using RCMendoza.Helpers;
using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Request;
using RCMendoza.Response;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RCMendoza.Models.Services
{
    public class LoginService : ILoginService
    {
        public async Task<Result> AuthLogin(Login oModel)
        {
            Result result = null;
            try
            {
                result = new Result();
                string Password = Encrypt.GetSHA256(oModel.Password);
                using (var db = new DBContext())
                {

                    var oUser = db.Usuarios.Where(u=>u.Email == oModel.Usuario && u.Contrasenia == Password).FirstOrDefault();
                    if(oUser != null)
                    {
                        result.Success = true;
                        result.Message = "Usuario Valido";
                        result.Data = new Usuario 
                        {
                            IdUsuario = oUser.IdUsuario,
                            Nombres = oUser.Nombres,
                            Apellidopaterno = oUser.Apellidopaterno,
                            Apellidomaterno = oUser.Apellidomaterno,
                            Email = oUser.Email,
                            Direccion  = oUser.Direccion,
                            Telefono = oUser.Telefono,
                            FkRoles = oUser.FkRoles,
                        };
                    }
                    else { throw new Exception("Usuario o Contraseña incorrecta."); }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = null;
            }
            return result;
        }
    }
}
