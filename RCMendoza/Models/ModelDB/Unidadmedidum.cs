using System;
using System.Collections.Generic;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class Unidadmedidum
    {
        public Unidadmedidum()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdUnidadmedida { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
