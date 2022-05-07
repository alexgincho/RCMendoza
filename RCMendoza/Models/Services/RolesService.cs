using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCMendoza.Models.Services
{
    public class RolesService : IRolesService
    {
        public List<Role> GetRoles()
        {
            List<Role> result = null;
            string error = "";
            try
            {
                using (var db = new DBContext())
                {
                    var lst = db.Roles.ToList();

                    if (lst.Count() > 0) { result = lst; }
                    else { throw new Exception("Error. Datos Vacios."); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }
    }
}
