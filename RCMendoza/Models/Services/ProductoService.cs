using RCMendoza.Models.Interfaces;
using RCMendoza.Models.ModelDB;
using RCMendoza.Response;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RCMendoza.Models.Services
{
    public class ProductoService : IProductoService
    {
        public async Task<Result> CreateProducto(Producto producto)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModel = new Producto();
                    oModel.Codigo = producto.Codigo;
                    oModel.Nombre = producto.Nombre;
                    oModel.Descripcion = producto.Descripcion;
                    oModel.Precioventa = producto.Precioventa;
                    oModel.Preciocompra = producto.Preciocompra;
                    oModel.Cantidad = producto.Cantidad;
                    oModel.Fechavencimiento = producto.Fechavencimiento;
                    oModel.Fecharegistro = DateTime.Now;
                    oModel.FkCategoria = producto.FkCategoria;
                    oModel.FkUnidadmedida = producto.FkUnidadmedida;
                    oModel.FkProveedor = producto.FkProveedor;

                    db.Productos.Add(oModel);
                    await db.SaveChangesAsync();

                    if (oModel.IdProducto != 0)
                    {
                        result.Success = true;
                        result.Message = "Producto Creado";
                        result.Data = new Producto { Codigo = oModel.Codigo, Nombre = oModel.Nombre };
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

        public async Task<Result> DeleteProducto(int id)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oModel = db.Productos.Where(x => x.IdProducto == id).FirstOrDefault();
                    if (oModel != null)
                    {
                        db.Remove<Producto>(oModel);
                        await db.SaveChangesAsync();
                        result.Success = true;
                        result.Message = "Producto Eliminado.";
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
                    var oModel = (from usu in db.Productos
                                 // join r in db.Roles on usu.FkRoles equals r.IdRoles
                                 // join d in db.Tipodocumentos on usu.FkTipodocumento equals d.IdTipodocumento
                                  //where usu.IdUsuario == id
                                  select new Producto
                                  {
                                      IdProducto = usu.IdProducto,
                                      Codigo = usu.Codigo,
                                      Nombre = usu.Nombre,
                                      Descripcion = usu.Descripcion,
                                      Precioventa = usu.Precioventa,
                                      Preciocompra = usu.Preciocompra,
                                      Cantidad = usu.Cantidad,
                                      Fechavencimiento = usu.Fechavencimiento,
                                      Fecharegistro = usu.Fecharegistro,
                                      FkCategoria = usu.FkCategoria,
                                      FkUnidadmedida = usu.FkUnidadmedida,
                                      FkProveedor = usu.FkProveedor,
                                      FkCategoriaNavigation = usu.FkCategoriaNavigation,
                                      FkProveedorNavigation = usu.FkProveedorNavigation,
                                      FkUnidadmedidaNavigation = usu.FkUnidadmedidaNavigation
                                  }).FirstOrDefault();
                    if (oModel != null)
                    {
                        result.Success = true;
                        result.Message = "Usuario Encontrado";
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
                    var oModelList = (
                        from produc in db.Productos
                       // join r in db.Roles on produc.FkRoles equals r.IdRoles
                        //join d in db.Tipodocumentos on produc.FkTipodocumento equals d.IdTipodocumento
                        select new Producto 
                        {
                            IdProducto = produc.IdProducto,
                            Codigo = produc.Codigo,
                            Nombre = produc.Nombre,
                            Descripcion = produc.Descripcion,
                            Precioventa = produc.Precioventa,   
                            Preciocompra = produc.Preciocompra,
                            Cantidad = produc.Cantidad,
                            Fechavencimiento = produc.Fechavencimiento,
                            Fecharegistro = produc.Fecharegistro,
                            FkCategoria = produc.FkCategoria,
                            FkUnidadmedida = produc.FkUnidadmedida,
                            FkProveedor = produc.FkProveedor,
                            FkCategoriaNavigation = produc.FkCategoriaNavigation,
                            FkProveedorNavigation = produc.FkProveedorNavigation,
                            FkUnidadmedidaNavigation = produc.FkUnidadmedidaNavigation
                        }).ToList();
                    if (oModelList.Count() > 0)
                    {
                        result.Success = true;
                        result.Message = "";
                        result.Data = oModelList;
                    }
                    else { throw new Exception("No hay registros de productos"); }
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

        public async Task<Result> UpdateProducto(Producto producto)
        {
            Result result = null;
            try
            {
                result = new Result();
                using (var db = new DBContext())
                {
                    var oProducto = db.Productos.Find(producto.IdProducto);
                    if (oProducto.IdProducto != 0)
                    {
                        oProducto.Nombre = producto.Nombre;
                        oProducto.Descripcion = producto.Descripcion;
                        oProducto.Precioventa = producto.Precioventa;
                        oProducto.Preciocompra = producto.Preciocompra;
                        oProducto.Cantidad = producto.Cantidad;
                        oProducto.FkCategoria = producto.FkCategoria;
                        oProducto.FkUnidadmedida = producto.FkUnidadmedida;
                        oProducto.FkProveedor = producto.FkProveedor;

                        db.Update<Producto>(oProducto);
                        await db.SaveChangesAsync();
                        if (oProducto != null)
                        {
                            result.Success = true;
                            result.Message = "Datos Actualizados.";
                            result.Data = new Producto { Codigo = oProducto.Codigo };
                        }
                        else { throw new Exception(); }
                    }
                    else { throw new Exception("Producto no existe en la BD."); }
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
