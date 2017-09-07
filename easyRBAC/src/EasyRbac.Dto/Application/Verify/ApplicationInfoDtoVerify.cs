using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace EasyRbac.Dto.Application.Verify
{
    public class ApplicationInfoDtoVerify: AbstractValidator<ApplicationInfoDto>
    {
        public ApplicationInfoDtoVerify()
        {
            RuleFor(x => x.AppName).NotEmpty().MaximumLength(45);
            RuleFor(x => x.AppCode).NotEmpty().MaximumLength(45);
            RuleFor(x => x.Descript).NotEmpty().MaximumLength(200);
            RuleFor(x => x.CallbackUrl).MaximumLength(200);
        }
    }
}
