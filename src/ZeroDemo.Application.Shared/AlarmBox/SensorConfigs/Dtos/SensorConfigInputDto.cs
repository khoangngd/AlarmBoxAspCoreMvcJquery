using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeroDemo.Authorization.Users.Dto;
using ZeroDemo.Dto;

namespace ZeroDemo.AlarmBox.SensorConfigs.Dtos
{
    public class SensorConfigInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public long? BoxId { get; set; }

        public long? SensorId { get; set; }

        public double? HighValue { get; set; }

        public double? LowValue { get; set; }

        public double? TargetValue { get; set; }

        public string InrangeColor { get; set; }

        public string OutrangeColor { get; set; }

        public bool? IsAlarm { get; set; }

        public string AlarmMessage { get; set; }

        public int? BoxPort { get; set; }


        public long? Id { get; set; }

        public string Filter { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime";
            }

            Filter = Filter?.Trim();
        }

    }
}
