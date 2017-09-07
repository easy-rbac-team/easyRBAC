using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace EasyRbac.Dto.AppResource.Verify
{
    public class ResouceVerify : AbstractValidator<AppResourceDto>
    {
        public ResouceVerify()
        {
            RuleFor(x => x.ApplicationId).GreaterThan(int.MaxValue);
            RuleFor(x => x.ResouceName).NotEmpty();
            RuleFor(x => x.ResouceCode).NotEmpty().MaximumLength(40);
            RuleFor(x => x.Url).MaximumLength(100);
            RuleFor(x => x.IconUrl).MaximumLength(45);
            RuleFor(x => x.Parameters).MaximumLength(45);
            RuleFor(x => x.Describe).MaximumLength(200);
        }
    }
}
