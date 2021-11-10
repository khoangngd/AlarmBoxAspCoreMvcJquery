using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ZeroDemo
{
    public class ZeroDemoCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoCoreSharedModule).GetAssembly());
        }
    }
}