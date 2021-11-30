using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeroDemo.AlarmBox.SensorConfigs.Dtos;

namespace ZeroDemo.AlarmBox.Sensors.Dtos
{
    public class SensorDto : NullableIdDto<long>
    {
        public string SensorName { get; set; }

        public double HighValueDefault { get; set; }

        public double LowValueDefault { get; set; }

        public double TargetValueDefault { get; set; }

        public ICollection<SensorConfigNoSensorDto> SensorConfigs { get; set; }

        public DateTime CreationTime { get; set; }

        public SensorDto() 
        {
        }
    }
}
