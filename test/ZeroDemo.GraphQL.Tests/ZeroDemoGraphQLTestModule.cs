using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ZeroDemo.Configure;
using ZeroDemo.Startup;
using ZeroDemo.Test.Base;

namespace ZeroDemo.GraphQL.Tests
{
    [DependsOn(
        typeof(ZeroDemoGraphQLModule),
        typeof(ZeroDemoTestBaseModule))]
    public class ZeroDemoGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoGraphQLTestModule).GetAssembly());
        }
    }
}