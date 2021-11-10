using System.Threading.Tasks;
using Abp.Webhooks;

namespace ZeroDemo.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
