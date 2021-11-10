using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ZeroDemo
{
    [DependsOn(typeof(ZeroDemoCoreSharedModule))]
    public class ZeroDemoApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoApplicationSharedModule).GetAssembly());
        }
    }
}