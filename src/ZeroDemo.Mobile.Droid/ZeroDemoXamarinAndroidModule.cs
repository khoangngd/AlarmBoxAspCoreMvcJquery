using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ZeroDemo
{
    [DependsOn(typeof(ZeroDemoXamarinSharedModule))]
    public class ZeroDemoXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoXamarinAndroidModule).GetAssembly());
        }
    }
}