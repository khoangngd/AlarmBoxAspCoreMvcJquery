using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Boxes.Dtos;

namespace ZeroDemo.Web.Areas.App.Models.Box
{
    //Ko có cũng được
    [AutoMapFrom(typeof(PagedResultDto<BoxDto>))]
    public class BoxViewModel : PagedResultDto<BoxDto>
    {
        //public string FilterText { get; set; }
        //public bool? IsDeletedBox { get; set; }
    }

}
