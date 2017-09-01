using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Dto.User;
using FluentValidation;

namespace EasyRbac.Dto.FluentValidate
{
    public class CreateUserDtoVerify:AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoVerify()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(customer => customer.Password).Equal(customer => customer.ConfirmPassword);
        }
    }
}
