using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Sensors.Dtos;

namespace ZeroDemo.Web.Areas.App.Models.Sensor
{
    [AutoMapFrom(typeof(SensorDto))]
    public class CreateOrEditSensorModalViewModel : SensorInputDto
    {
        public bool IsEditMode => Id.HasValue;
    }
}
