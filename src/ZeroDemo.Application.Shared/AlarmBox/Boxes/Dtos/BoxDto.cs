using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZeroDemo.AlarmBox.Boxes.Dtos
{
    public class BoxDto : NullableIdDto<long>
    {
        public string BoxName { get; set; }

        public string Location { get; set; }

        public int MaxBoxPort { get; set; }

        public int MaxBoxManagerPort { get; set; }

        public DateTime CreationTime { get; set; }

        public BoxDto() 
        {
            MaxBoxPort = BoxConsts.MaxBoxPortDefault;
            MaxBoxManagerPort = BoxConsts.MaxBoxManagerPortDefault;
        }
    }
}
