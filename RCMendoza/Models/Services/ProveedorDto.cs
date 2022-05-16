using RCMendoza.Models.ModelDB;

namespace RCMendoza.Models.Services
{
    public class ProveedorDto
    {
        public object[] IdProveedor { get; internal set; }
        public object Numerodoc { get; internal set; }
        public int? FkTipodocumento { get; internal set; }
        public string Telefono { get; internal set; }
        public string Razonsocial { get; internal set; }
        public Distrito FkDistritoNavigation { get; internal set; }
        public string Email { get; internal set; }
        public string Direccion { get; internal set; }
    }
}