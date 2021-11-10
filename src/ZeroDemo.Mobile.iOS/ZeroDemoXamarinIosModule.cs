using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ZeroDemo
{
    [DependsOn(typeof(ZeroDemoXamarinSharedModule))]
    public class ZeroDemoXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoXamarinIosModule).GetAssembly());
        }
    }
}