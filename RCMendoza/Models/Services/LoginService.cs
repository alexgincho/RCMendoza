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
        public async Task<Usuario> AuthLogin(Login oModel)
        {
            Usuario result = null;
            try
            {         
                string Password = Encrypt.GetSHA256(oModel.Password);
                using (var db = new DBContext())
                {

                    var oUser = db.Usuarios.Where(u=>u.Email == oModel.Usuario && u.Contrasenia == Password).FirstOrDefault();
                    if(oUser != null)
                    {
                        result = new Usuario();
                        result = oUser;
                    }
                    else { throw new Exception(); }
                }
            }
            catch (Exception ex)
            {
                string error =  ex.Message;
            }
            return result;
        }
    }
}
