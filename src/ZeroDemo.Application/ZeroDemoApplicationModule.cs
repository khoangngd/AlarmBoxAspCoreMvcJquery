using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ZeroDemo.Authorization;

namespace ZeroDemo
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(ZeroDemoApplicationSharedModule),
        typeof(ZeroDemoCoreModule)
        )]
    public class ZeroDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoApplicationModule).GetAssembly());
        }
    }
}