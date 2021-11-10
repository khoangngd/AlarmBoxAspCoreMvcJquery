using Microsoft.AspNetCore.Mvc;
using ZeroDemo.Web.Controllers;

namespace ZeroDemo.Web.Public.Controllers
{
    public class AboutController : ZeroDemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}