using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RCMendoza.Models.Services
{
    public class UsuarioService : IUsuarioService
    {
        public async Task<Result> CreateUsuario(Usuario usuario)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModel = new Usuario();
                    oModel.Numerodoc = usuario.Numerodoc;   
                    oModel.Nombres = usuario.Nombres;
                    oModel.Apellidopaterno = usuario.Apellidopaterno;
                    oModel.Apellidomaterno = usuario.Apellidomaterno;
                    oModel.Email = usuario.Email;
                    oModel.Direccion = usuario.Direccion;   
                    oModel.Telefono = usuario.Telefono; 
                    oModel.Contrasenia = usuario.Contrasenia;   
                    oModel.FkRoles = usuario.FkRoles;   
                    oModel.FkTipodocumento = usuario.FkTipodocumento;
                    oModel.Fecharegistro = DateTime.Now;

                    db.Usuarios.Add(oModel);
                    await db.SaveChangesAsync();
                    
                    if (oModel.IdUsuario != 0)
                    {
                        result.Success = true;
                        result.Message = "Usuario Creado";
                        result.Data = new Usuario { Nombres = oModel.Nombres , Apellidopaterno = oModel.Apellidopaterno };
                    }
                    else { throw new Exception(); }

                }
            }
            catch (Exception ex)
            {
                result.Success =false;  
                result.Message = ex.Message;    
                result.Data = ex.Data;
            }
            return result;  
        }

        public async Task<Result> DeleteUsuario(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModel = db.Usuarios.Where(x => x.IdUsuario == id).FirstOrDefault();
                    if(oModel != null)
                    {
                        db.Remove<Usuario>(oModel);
                        await db.SaveChangesAsync();
                        result.Success = true;
                        result.Message = "Usuario Eliminado.";
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
            return result;
        }
        public Usuario Get(int id)
        {
            Usuario usuario = null;
            try
            {
                usuario = new Usuario();
                using (var db = new DBContext())
                {
                    #region consulta low
                    //var oModel = db.Usuarios.Where(u => u.IdUsuario == id).FirstOrDefault();
                    //oModel.FkRolesNavigation = db.Roles.Find(oModel.FkRoles);
                    //oModel.FkTipodocumentoNavigation = db.Tipodocumentos.Find(oModel.FkTipodocumento);
                    #endregion
                    var oModel = (from usu in db.Usuarios
                                   join r in db.Roles on usu.FkRoles equals r.IdRoles
                                   join d in db.Tipodocumentos on usu.FkTipodocumento equals d.IdTipodocumento
                                   where usu.IdUsuario == id
                                   select new Usuario
                                   {
                                       IdUsuario = usu.IdUsuario,
                                       Numerodoc = usu.Numerodoc,
                                       Nombres = usu.Nombres,
                                       Apellidopaterno = usu.Apellidopaterno,
                                       Apellidomaterno = usu.Apellidomaterno,
                                       Direccion = usu.Direccion,
                                       Telefono = usu.Telefono,
                                       Email = usu.Email,
                                       Contrasenia = usu.Contrasenia,
                                       Fecharegistro = usu.Fecharegistro,
                                       FkRoles = usu.FkRoles,
                                       FkRolesNavigation = usu.FkRolesNavigation,
                                       FkTipodocumento = usu.FkTipodocumento,
                                       FkTipodocumentoNavigation = usu.FkTipodocumentoNavigation
                                   }).FirstOrDefault();
                    if(oModel != null)
                    {
                        // result.Success = true;
                        // result.Message = "Usuario Encontrado";
                        usuario = oModel;
                    }
                    else { throw new Exception(); }
                }
            }
            catch (Exception ex)
            {
                // result.Success = false;
                // result.Message = ex.Message;
                usuario = null;
            }
            return usuario;
        }

        public async Task<Result> GetAll()
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModelList = (from usu in db.Usuarios
                                     join r in db.Roles on usu.FkRoles equals r.IdRoles
                                     join d in db.Tipodocumentos on usu.FkTipodocumento equals d.IdTipodocumento
                                     select new Usuario
                                     {
                                         IdUsuario = usu.IdUsuario,
                                         Numerodoc = usu.Numerodoc,
                                         Nombres = usu.Nombres,
                                         Apellidopaterno = usu.Apellidopaterno,
                                         Apellidomaterno = usu.Apellidomaterno,
                                         Direccion = usu.Direccion,
                                         Telefono = usu.Telefono,
                                         Email = usu.Email,
                                         Contrasenia = usu.Contrasenia,
                                         Fecharegistro = usu.Fecharegistro,
                                         FkRoles = usu.FkRoles,
                                         FkRolesNavigation = usu.FkRolesNavigation,
                                         FkTipodocumento = usu.FkTipodocumento,
                                         FkTipodocumentoNavigation = usu.FkTipodocumentoNavigation
                                     }).ToList();
                    if(oModelList.Count() > 0)
                    {
                        result.Success = true;
                        result.Message = "";
                        result.Data = oModelList;
                    }
                    else { throw new Exception("No hay registros de usuarios"); }
                }
            }
            catch (Exception ex)
            {
                result.Success =false;
                result.Message = ex.Message;
                result.Data = null;
            }
            return result;
        }

        public async Task<Result> UpdateUsuario(Usuario usuario)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oUsuario = db.Usuarios.Find(usuario.IdUsuario);
                    if(oUsuario.IdUsuario != 0)
                    {
                        oUsuario.Numerodoc = usuario.Numerodoc;
                        oUsuario.Nombres = usuario.Nombres; 
                        oUsuario.Apellidopaterno = usuario.Apellidopaterno;
                        oUsuario.Apellidomaterno = usuario.Apellidomaterno;
                        oUsuario.Direccion = usuario.Direccion; 
                        oUsuario.Telefono = usuario.Telefono;   
                        oUsuario.FkRoles = usuario.FkRoles;
                        oUsuario.FkTipodocumento = usuario.FkTipodocumento;

                        db.Update<Usuario>(oUsuario);
                        await db.SaveChangesAsync();
                        if(oUsuario != null) 
                        { 
                            result.Success = true; 
                            result.Message = "Datos Actualizados.";
                            result.Data = new Usuario { Numerodoc = oUsuario.Numerodoc };
                        } 
                        else { throw new Exception(); }
                    }
                    else { throw new Exception("Usuario no existe en la BD."); }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex.Data;
            }
            return result;
        }
    }
}
