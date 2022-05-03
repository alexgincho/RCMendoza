using System;
using System.Collections.Generic;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double? Precioventa { get; set; }
        public double? Preciocompra { get; set; }
        public int? Cantidad { get; set; }
        public DateTime? Fechavencimiento { get; set; }
        public DateTime? Fecharegistro { get; set; }
        public int? FkCategoria { get; set; }
        public int? FkUnidadmedida { get; set; }
        public int? FkProveedor { get; set; }

        public virtual Categorium FkCategoriaNavigation { get; set; }
        public virtual Proveedor FkProveedorNavigation { get; set; }
        public virtual Unidadmedidum FkUnidadmedidaNavigation { get; set; }
    }
}
