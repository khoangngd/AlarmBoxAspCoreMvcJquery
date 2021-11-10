using System.Threading.Tasks;
using ZeroDemo.Sessions.Dto;

namespace ZeroDemo.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
