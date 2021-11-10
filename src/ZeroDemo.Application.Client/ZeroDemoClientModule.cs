using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ZeroDemo
{
    public class ZeroDemoClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroDemoClientModule).GetAssembly());
        }
    }
}
