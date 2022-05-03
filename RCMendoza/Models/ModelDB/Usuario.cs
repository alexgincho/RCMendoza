using System;
using System.Collections.Generic;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidopaterno { get; set; }
        public string Apellidomaterno { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public DateTime? Fecharegistro { get; set; }
        public int? FkTipodocumento { get; set; }
        public int? FkRoles { get; set; }
        public string Numerodoc { get; set; }

        public virtual Role FkRolesNavigation { get; set; }
        public virtual Tipodocumento FkTipodocumentoNavigation { get; set; }
    }
}
