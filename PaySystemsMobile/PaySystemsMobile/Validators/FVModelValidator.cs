using FluentValidation;
using PaySystemsMobile.Models;
using PaySystemsMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySystemsMobile.Validators
{
    public class FVModelValidator : AbstractValidator<UsuarioModel>
    {
        public FVModelValidator()
        {
            RuleFor(x => x.NombreCompleto).NotEmpty()
                .WithMessage("El nombre es un campo obligatorio");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email es un campo obligatorio")
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(x => x.Contraseña).MinimumLength(6)
                .WithMessage("La contraseña debe contener al menos 6 dígitos");

            RuleFor(x => x.Telefono).NotEmpty()
                .WithMessage("El teléfono es un campo obligatorio");

            RuleFor(x => x.NombreUsuario).NotEmpty()
                .WithMessage("El usuario es un campo obligatorio");
        }
    }
}
