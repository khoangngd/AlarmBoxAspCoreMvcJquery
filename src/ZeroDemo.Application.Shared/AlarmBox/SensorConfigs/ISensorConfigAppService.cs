using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.SensorConfigs.Dtos;

namespace ZeroDemo.AlarmBox.SensorConfigs
{
    public interface ISensorConfigAppService : IApplicationService
    {
        Task<PagedResultDto<SensorConfigDto>> GetAll(SensorConfigInputDto input);

        Task<SensorConfigDto> GetById(NullableIdDto<long> input);

        Task CreateOrUpdate(SensorConfigInputDto input);

        Task Delete(EntityDto<long> input);
    }
}
