using FluentValidation;
using PaySystemsMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySystemsMobile.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email es un campo obligatorio")
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(x => x.Password).MinimumLength(6)
                .WithMessage("La contraseña debe contener al menos 6 dígitos");
        }
    }
}
