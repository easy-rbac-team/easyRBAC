using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRbac.Dto.UserLogin.Verify
{
    public class UserLoginDtoVerify : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoVerify()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(200);
            RuleFor(x => x.AppCode).NotEmpty().MaximumLength(50);
        }
    }
}
