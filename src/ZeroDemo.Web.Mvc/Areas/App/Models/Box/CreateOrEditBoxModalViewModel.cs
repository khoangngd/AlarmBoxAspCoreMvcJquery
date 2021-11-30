using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Boxes.Dtos;
using ZeroDemo.AlarmBox.Sensors.Dtos;

namespace ZeroDemo.Web.Areas.App.Models.Box
{
    [AutoMapFrom(typeof(BoxDto))]
    public class CreateOrEditBoxModalViewModel : BoxInputDto
    {
        public bool IsEditMode => Id.HasValue;

        //public SensorConfigInputDto SensorFilter { get; set; }

        //public int? ManagerPort { get; set; }
    }
}
