using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Dto.User;
using FluentValidation;

namespace EasyRbac.Dto.FluentValidate
{
    class ChangePwdDtoVerify:AbstractValidator<ChangePwd>
    {
        public ChangePwdDtoVerify()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
        }
    }
}
