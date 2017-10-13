using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Utils.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyUtility.Commons.Encrypt;
using MyUtility.Commons.IdGenerate;
using MyUtility.Commons.NumberConvert;

namespace EasyRbac.Utils.Denpendency
{
    public static class UtilsDenpendency
    {
        public static IServiceCollection AddUtils(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEncryptHelper, EncryptHelper>();
            //serviceCollection.Configure<IdGenerateConfig>("IdGenerate", opt=>{});
            IConfiguration idGenerateConfiguration = (configuration.GetSection("IdGenerate") as IConfiguration) ?? new ConfigurationBuilder().Build();
            services.Configure<IdGenerateConfig>(idGenerateConfiguration);
            services.AddSingleton<IIdGenerator>(
                p =>
                {
                    var config = p.GetRequiredService<IOptions<IdGenerateConfig>>();
                    return new EasyGenerator(config.Value.NodeId, config.Value.TimeBack);
                });
            services.AddSingleton<INumberConvert>((p)=>new NumberConvert("0123456789abcdefghijklmnopqrstuvwxyz"));

            return services;
        }
    }
}
