using System;
using System.Linq;
using System.Threading.Tasks;
using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;

namespace RCMendoza.Models.Services
{
    public class ProveedorService : IProveedorService
    {

        public async Task<Result> CreateProveedor(Proveedor proveedor)
        {
            Result result = null;
            try
            {
                using (var db = new DBContext())
                {
                    result = new Result();
                    Proveedor oModel = new Proveedor();
                    oModel.Razonsocial = proveedor.Razonsocial;
                    oModel.Numerodoc = proveedor.Numerodoc;
                    oModel.Direccion = proveedor.Direccion;
                    oModel.Email = proveedor.Email;
                    oModel.Telefono = proveedor.Telefono;
                    oModel.FkTipodocumento = proveedor.FkTipodocumento;
                    oModel.FkDistrito = proveedor.FkDistrito;

                    db.Add(oModel);
                    await db.SaveChangesAsync();
                    if (oModel.IdProveedor != 0)
                    {
                        result.Success = true;
                        result.Message = "Proveedor Registrado";
                        result.Data = new { Ruc = oModel.Numerodoc, RazonSocial = oModel.Razonsocial };
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
        public async Task<Result> DeleteProveedor(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModel = db.Proveedors.Where(x => x.IdProveedor == id).FirstOrDefault();
                    if (oModel != null)
                    {
                        db.Remove<Proveedor>(oModel);
                        await db.SaveChangesAsync();
                        result.Success = true;
                        result.Message = "Proveedor Eliminado.";
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
        public Proveedor Get(int id)
        {
            Proveedor proveedor = null;
            try
            {
                proveedor = new Proveedor();
                using (var db = new DBContext())
                {
                    #region consulta low
                    //var oModel = db.Proveedors.Where(u => u.IdProveedor== id).FirstOrDefault();
                    //oModel.FkDistritoNavigation = dbDistrito.Find(oModel.FkDistrito);
                    //oModel.FkTipodocumentoNavigation = db.Tipodocumentos.Find(oModel.FkTipodocumento);
                    #endregion
                    var oModel = (from Prov in db.Proveedors
                                  join r in db.Roles on Prov.FkDistrito equals r.IdDistrito
                                  join d in db.Tipodocumentos on Prov.FkTipodocumento equals d.IdTipodocumento
                                  where Prov.IdProveedor == id
                                  select new Proveedor
                                  {
                                      Razonsocial = Prov.Razonsocial,
                                      Numerodoc = Prov.Numerodoc,
                                      Direccion= Prov.Direccion,
                                      Email= Prov.Email,
                                      Telefono =Prov.Telefono,
                                      FkTipodocumento=Prov.FkTipodocumento,
                                      FkDistrito=Prov.FkDistrito,
                                      FkDistritoNavigation = Prov.FkNavigation,
                                      FkTipodocumentoNavigation=Prov.FkTipodocumentoNavigation,
                                  }).FirstOrDefault();
                    if (oModel != null)
                    {
                        // result.Success = true;
                        // result.Message = "Proveedor Encontrado";
                        proveedor= oModel;
                    }
                    else { throw new Exception(); }
                }
            }
            catch (Exception)
            {
                // result.Success = false;
                // result.Message = ex.Message;
                proveedor = null;
            }
            return proveedor;
        }

        public Result GetAll()
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModelList = (from Prov in db.Proveedors
                                      join r in db.Roles on Prov.FkDistrito equals r.IdDistrito
                                      join d in db.Tipodocumentos on Prov.FkTipodocumento equals d.IdTipodocumento
                                      select new Proveedor
                                      {
                                          Razonsocial = Prov.Razonsocial,
                                          Numerodoc = Prov.Numerodoc,
                                          Direccion = Prov.Direccion,
                                          Email = Prov.Email,
                                          Telefono = Prov.Telefono,
                                          FkTipodocumento = Prov.FkTipodocumento,
                                          FkDistrito = Prov.FkDistrito,
                                          FkDistritoNavigation = Prov.FkNavigation,
                                          FkTipodocumentoNavigation = Prov.FkTipodocumentoNavigation,
                                      }).ToList();
                    if (oModelList.Count() > 0)
                    {
                        result.Success = true;
                        result.Message = "";
                        result.Data = oModelList;
                    }
                    else { throw new Exception("No hay registros de proveedors"); }
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

        public async Task<Result> UpdateProveedor(ProveedorDto proveedor)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oProveedor = db.Proveedors.Find(proveedor.IdProveedor);
                    if (oProveedor.IdProveedor != 0)
                    {
                        oProveedor.Razonsocial = proveedor.Razonsocial;
                        oProveedor.Numerodoc = (string)proveedor.Numerodoc;
                        oProveedor.Direccion = proveedor.Direccion;
                        oProveedor.Email = proveedor.Email;
                        oProveedor.Telefono = proveedor.Telefono;
                        oProveedor.FkTipodocumento = proveedor.FkTipodocumento;
                        oProveedor.FkDistritoNavigation = proveedor.FkDistritoNavigation;

                        db.Update<Proveedor>(oProveedor);
                        await db.SaveChangesAsync();
                        if (oProveedor != null)
                        {
                            result.Success = true;
                            result.Message = "Datos Actualizados.";
                            result.Data = new Proveedor { Numerodoc = oProveedor.Numerodoc};
                        }
                        else { throw new Exception(); }
                    }
                    else { throw new Exception("Proveedor no existe en la BD."); }
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
