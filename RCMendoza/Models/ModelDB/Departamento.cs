using System;
using System.Collections.Generic;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class Departamento
    {
        public Departamento()
        {
            Provincia = new HashSet<Provincium>();
        }

        public int IdDepartamento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Provincium> Provincia { get; set; }
    }
}
