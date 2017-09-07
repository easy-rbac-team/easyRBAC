using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRbac.Dto.Mapper
{
    public static class AutoMapperDependencyInject
    {
        public static void UseDtoAutoMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
