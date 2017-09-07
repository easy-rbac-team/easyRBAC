using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Dto.User;
using FluentValidation;

namespace EasyRbac.Dto.Role.Verify
{
    public class RoleDtoVerify : AbstractValidator<RoleDto>
    {
        public RoleDtoVerify()
        {
            RuleFor(x => x.RoleName).NotEmpty().MaximumLength(40);
        }
    }
}
