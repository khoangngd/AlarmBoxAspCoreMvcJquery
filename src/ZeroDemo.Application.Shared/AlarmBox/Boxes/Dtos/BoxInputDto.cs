using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZeroDemo.Authorization.Users.Dto;
using ZeroDemo.Dto;

namespace ZeroDemo.AlarmBox.Boxes.Dtos
{
    public class BoxInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string BoxName { get; set; }

        public string Location { get; set; }

        public int? MaxBoxPort { get; set; }

        public int? MaxBoxManagerPort { get; set; }


        public long? Id { get; set; }

        public string Filter { get; set; }

        public bool? IsDeletedBox { get; set; }

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
