using RCMendoza.Helpers;
using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RCMendoza.Models.Services
{
    public class ClienteService : IClienteService
    {
        public async Task<Result> CreateCliente(Usuario cliente)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModel = new Usuario();
                    oModel.Numerodoc = cliente.Numerodoc;
                    oModel.Nombres = cliente.Nombres;
                    oModel.Apellidopaterno = cliente.Apellidopaterno;
                    oModel.Apellidomaterno = cliente.Apellidomaterno;
                    oModel.Email = cliente.Email;
                    oModel.Direccion = cliente.Direccion;
                    oModel.Telefono = cliente.Telefono;
                    oModel.Contrasenia = Encrypt.GetSHA256(cliente.Contrasenia);
                    oModel.FkRoles = cliente.FkRoles = '2';
                    oModel.FkTipodocumento = cliente.FkTipodocumento;
                    oModel.Fecharegistro = DateTime.Now;

                    db.Usuarios.Add(oModel);
                    await db.SaveChangesAsync();

                    if (oModel.IdUsuario != 0)
                    {
                        result.Success = true;
                        result.Message = "Cliente Creado";
                        result.Data = new Usuario { Nombres = oModel.Nombres, Apellidopaterno = oModel.Apellidopaterno };
                    }
                    else { throw new Exception(); }

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

        public Task<Result> DeleteCliente(int id)
        {
            throw new NotImplementedException();
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
                    if (oModel != null)
                    {
                        db.Remove<Usuario>(oModel);
                        await db.SaveChangesAsync();
                        result.Success = true;
                        result.Message = "Cliente Eliminado.";
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
        public async Task<Result> Get(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
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
                    if (oModel != null)
                    {
                        result.Success = true;
                        result.Message = "Cliente Encontrado";
                        result.Data = oModel;
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
                    if (oModelList.Count() > 0)
                    {
                        result.Success = true;
                        result.Message = "";
                        result.Data = oModelList;
                    }
                    else { throw new Exception("No hay registros de Clientes"); }
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

        public Task<Result> UpdateCliente(Usuario cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> UpdateUsuario(Usuario cliente)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oUsuario = db.Usuarios.Find(cliente.IdUsuario);
                    if (oUsuario.IdUsuario != 0)
                    {
                        oUsuario.Numerodoc = cliente.Numerodoc;
                        oUsuario.Nombres = cliente.Nombres;
                        oUsuario.Apellidopaterno = cliente.Apellidopaterno;
                        oUsuario.Apellidomaterno = cliente.Apellidomaterno;
                        oUsuario.Direccion = cliente.Direccion;
                        oUsuario.Telefono = cliente.Telefono;
                        oUsuario.FkRoles = cliente.FkRoles;
                        oUsuario.FkTipodocumento = cliente.FkTipodocumento;

                        db.Update<Usuario>(oUsuario);
                        await db.SaveChangesAsync();
                        if (oUsuario != null)
                        {
                            result.Success = true;
                            result.Message = "Datos Actualizados.";
                            result.Data = new Usuario { Numerodoc = oUsuario.Numerodoc };
                        }
                        else { throw new Exception(); }
                    }
                    else { throw new Exception("Cliente no existe en la BD."); }
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
