using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeroDemo.Authorization.Users.Dto;
using ZeroDemo.Dto;

namespace ZeroDemo.AlarmBox.Sensors.Dtos
{
    public class SensorInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SensorName { get; set; }

        public double? HighValueDefault { get; set; }

        public double? LowValueDefault { get; set; }

        public double? TargetValueDefault { get; set; }


        public long? Id { get; set; }

        public string Filter { get; set; }

        //public long? BoxId { get; set; }

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
