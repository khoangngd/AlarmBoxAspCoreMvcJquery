using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeroDemo.Authorization.Users.Dto;
using ZeroDemo.Dto;

namespace ZeroDemo.AlarmBox.BoxManagers.Dtos
{
    public class BoxManagerInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public long? BoxId { get; set; }

        public string ManagerName { get; set; }

        public string ManagerPhoneNumber { get; set; }

        public string ManagerEmail { get; set; }

        public int? ManagerPort { get; set; }

        public bool? IsAlarm { get; set; }


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
