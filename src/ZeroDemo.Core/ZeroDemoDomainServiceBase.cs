using Abp.Domain.Services;

namespace ZeroDemo
{
    public abstract class ZeroDemoDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected ZeroDemoDomainServiceBase()
        {
            LocalizationSourceName = ZeroDemoConsts.LocalizationSourceName;
        }
    }
}
