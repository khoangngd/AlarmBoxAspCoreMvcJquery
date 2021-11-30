using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeroDemo.AlarmBox.Boxes.Dtos;
using ZeroDemo.AlarmBox.Sensors.Dtos;

namespace ZeroDemo.AlarmBox.BoxManagers.Dtos
{
    public class BoxManagerDto : NullableIdDto<long>
    {
        public long BoxId { get; set; }
        public BoxDto Box { get; set; }

        public string ManagerName { get; set; }

        public string ManagerPhoneNumber { get; set; }

        public string ManagerEmail { get; set; }

        public int ManagerPort { get; set; }

        public bool IsAlarm { get; set; }

        public BoxManagerDto() 
        {
            IsAlarm = false;
        }
    }
}
