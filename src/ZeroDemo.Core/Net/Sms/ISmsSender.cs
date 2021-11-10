using System.Threading.Tasks;

namespace ZeroDemo.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}