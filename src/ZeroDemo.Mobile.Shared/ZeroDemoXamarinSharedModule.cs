using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ZeroDemo
{
    [DependsOn(typeof(ZeroDemoClientModule), typeof(AbpAutoMapperModule))]
    public class ZeroDemoXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoXamarinSharedModule).GetAssembly());
        }
    }
}