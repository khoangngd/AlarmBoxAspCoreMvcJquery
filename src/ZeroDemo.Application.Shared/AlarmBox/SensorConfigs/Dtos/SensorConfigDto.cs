using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeroDemo.AlarmBox.Boxes.Dtos;
using ZeroDemo.AlarmBox.Sensors.Dtos;

namespace ZeroDemo.AlarmBox.SensorConfigs.Dtos
{
    public class SensorConfigDto : NullableIdDto<long>
    {
        public long BoxId { get; set; }
        public BoxDto Box { get; set; }

        public long SensorId { get; set; }
        public SensorDto Sensor { get; set; }

        public double HighValue { get; set; }

        public double LowValue { get; set; }

        public double TargetValue { get; set; }

        public string InrangeColor { get; set; }

        public string OutrangeColor { get; set; }

        public bool IsAlarm { get; set; }

        public string AlarmMessage { get; set; }

        public int BoxPort { get; set; }

        public SensorConfigDto() 
        {
            IsAlarm = false;
        }
    }
}
