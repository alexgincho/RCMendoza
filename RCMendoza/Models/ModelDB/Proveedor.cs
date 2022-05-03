using System;
using System.Collections.Generic;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProveedor { get; set; }
        public string Numerodocumento { get; set; }
        public string Razonsocial { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int? FkTipodocumento { get; set; }
        public int? FkDistrito { get; set; }

        public virtual Distrito FkDistritoNavigation { get; set; }
        public virtual Tipodocumento FkTipodocumentoNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
