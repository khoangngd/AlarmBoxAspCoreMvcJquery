using System.Threading.Tasks;
using ZeroDemo.Authorization.Users;

namespace ZeroDemo.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
