using System;
using System.Collections.Generic;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class Distrito
    {
        public Distrito()
        {
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdDistrito { get; set; }
        public string Descripcion { get; set; }
        public int? FkProvincia { get; set; }

        public virtual Provincium FkProvinciaNavigation { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
