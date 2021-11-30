using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeroDemo.AlarmBox.Boxes;
using ZeroDemo.AlarmBox.Boxes.Dtos;
using ZeroDemo.Authorization;
using ZeroDemo.Web.Areas.App.Models.Box;
using ZeroDemo.Web.Controllers;

namespace ZeroDemo.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Box)]
    public class BoxController : ZeroDemoControllerBase
    {
        private readonly IBoxAppService _boxAppService;

        public BoxController(IBoxAppService boxAppService

            )
        {
            _boxAppService = boxAppService;

        }

        //public async Task<ActionResult> Index(BoxInputDto input)
        //{
        //    var output = await _boxAppService.GetAll(input);
        //    var model = ObjectMapper.Map<BoxViewModel>(output);
        //    //model.FilterText = input.Filter;
        //    //model.IsDeletedBox = input.IsDeletedBox;
        //    return View(model);
        //}
        //public async Task<ActionResult> Index(BoxInputDto input)
        //{
        //    var output = await _boxAppService.GetAll(input);
        //    var model = new BoxViewModel
        //    {
        //        //FilterText = Request.Query["filterText"],
        //    };
        //    return View(model);
        //}

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Box_Create, AppPermissions.Pages_Box_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            var output = await _boxAppService.GetById(new NullableIdDto<long> { Id = id });
            var viewModel = ObjectMapper.Map<CreateOrEditBoxModalViewModel>(output);
            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Box_Create, AppPermissions.Pages_Box_Edit)]
        public async Task<ActionResult> Detail (long id)
        {
            var output = await _boxAppService.GetById(new NullableIdDto<long> { Id = id });
            var viewModel = ObjectMapper.Map<CreateOrEditBoxModalViewModel>(output);
            return View(viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Box_Create, AppPermissions.Pages_SensorConfig_Create)]
        public async Task<PartialViewResult> AddSensorConfigModal(long id)
        {
            var output = await _boxAppService.GetById(new NullableIdDto<long> { Id = id });
            var viewModel = ObjectMapper.Map<CreateOrEditBoxModalViewModel>(output);
            return PartialView("_AddSensorConfigModal", viewModel);
        }

    }
}