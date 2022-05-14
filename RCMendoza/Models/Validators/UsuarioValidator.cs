using FluentValidation;
using RCMendoza.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCMendoza.Models.Validators
{
    public class UsuarioValidator:AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x=>x.Numerodoc).NotEmpty().WithMessage("Error. El Dni no puede ir vacio");
            RuleFor(x => x.Numerodoc).MaximumLength(8).WithMessage("Error. El Dni tiene un Maximo de 8 Digitos");
            RuleFor(x => x.Numerodoc).MinimumLength(8).WithMessage("Error. El Dni no contiene contiene 8 Digitos");
            RuleFor(x=>x.Nombres).NotEmpty().WithMessage("Error. El Nombre no puede ir vacio");
            RuleFor(x=>x.Apellidopaterno).NotEmpty().WithMessage("Error. El Apellido paterno no puede ir vacio");
            RuleFor(x=>x.Apellidomaterno).NotEmpty().WithMessage("Error. El Apellido materno no puede ir vacio");
            RuleFor(x=>x.Telefono).NotEmpty().WithMessage("Error. El Telefono no puede ir vacio");
            RuleFor(x=>x.Direccion).NotEmpty().WithMessage("Error. El Direccion no puede ir vacio");
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Error. El Email no puede ir vacio");
            RuleFor(x=>x.Contrasenia).NotEmpty().WithMessage("Error. El Contraseña no puede ir vacio");
        }
    }
}