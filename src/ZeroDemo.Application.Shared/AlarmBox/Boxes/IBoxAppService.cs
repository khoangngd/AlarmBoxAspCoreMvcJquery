using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Boxes.Dtos;

namespace ZeroDemo.AlarmBox.Boxes
{
    public interface IBoxAppService : IApplicationService
    {
        Task<PagedResultDto<BoxDto>> GetAll(BoxInputDto input);

        Task<BoxDto> GetById(NullableIdDto<long> input);

        Task CreateOrUpdate(BoxInputDto input);

        Task Delete(EntityDto<long> input);
    }
}
