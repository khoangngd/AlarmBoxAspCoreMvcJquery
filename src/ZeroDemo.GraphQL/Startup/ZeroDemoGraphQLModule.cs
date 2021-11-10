using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ZeroDemo.Startup
{
    [DependsOn(typeof(ZeroDemoCoreModule))]
    public class ZeroDemoGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}