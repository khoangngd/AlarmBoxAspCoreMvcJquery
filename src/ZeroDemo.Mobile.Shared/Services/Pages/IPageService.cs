using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZeroDemo.Services.Pages
{
    public interface IPageService
    {
        Page MainPage { get; set; }

        Task<Page> CreatePage(Type viewType, object navigationParameter);
    }
}
