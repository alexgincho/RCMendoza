using RCMendoza.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using RCMendoza.Response;
using System.Threading.Tasks;

namespace RCMendoza.Models.Interfaces
{
    public interface IDocumentosService
    {
        public List<Tipodocumento> GetDocumento();
    }
}