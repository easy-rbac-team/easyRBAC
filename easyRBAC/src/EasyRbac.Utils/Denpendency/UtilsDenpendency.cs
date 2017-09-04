using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Utils.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRbac.Utils.Denpendency
{
    public static class UtilsDenpendency
    {
        public static IServiceCollection AddUtils(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEncryptHelper, EncryptHelper>();
            serviceCollection.Configure<IdGenerateConfig>("IdGenerate",opt=>{});
            serviceCollection.AddSingleton<IIdGenerator, EasyGenerator>();

            return serviceCollection;
        }
    }
}
