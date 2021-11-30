using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Sensors;
using ZeroDemo.Authorization;
using ZeroDemo.Web.Areas.App.Models.Sensor;
using ZeroDemo.Web.Controllers;

namespace ZeroDemo.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Sensor)]
    public class SensorController : ZeroDemoControllerBase
    {
        private readonly ISensorAppService _sensorAppService;

        public SensorController(ISensorAppService sensorAppService

            )
        {
            _sensorAppService = sensorAppService;

        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Sensor_Create, AppPermissions.Pages_Sensor_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            var output = await _sensorAppService.GetById(new NullableIdDto<long> { Id = id });
            var viewModel = ObjectMapper.Map<CreateOrEditSensorModalViewModel>(output);
            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
