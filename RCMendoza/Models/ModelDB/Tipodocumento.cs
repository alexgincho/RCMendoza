using System;
using System.Collections.Generic;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class Tipodocumento
    {
        public Tipodocumento()
        {
            Proveedors = new HashSet<Proveedor>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdTipodocumento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Proveedor> Proveedors { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
