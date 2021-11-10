using Microsoft.Extensions.Configuration;

namespace ZeroDemo.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
