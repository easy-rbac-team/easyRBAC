using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Dto.User;
using FluentValidation;

namespace EasyRbac.Dto.UserScope.Validate
{
    public class UserScopeDtoVerify : AbstractValidator<UserScopeDto>
    {
        public UserScopeDtoVerify()
        {
            RuleFor(x => x.ResourceId).NotEmpty();
            RuleFor(x => x.AppId).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
