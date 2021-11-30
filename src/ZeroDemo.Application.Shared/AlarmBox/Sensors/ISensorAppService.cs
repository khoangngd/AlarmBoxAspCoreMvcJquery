using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Sensors.Dtos;

namespace ZeroDemo.AlarmBox.Sensors
{
    public interface ISensorAppService : IApplicationService
    {
        Task<PagedResultDto<SensorDto>> GetAll(SensorInputDto input);

        Task<SensorDto> GetById(NullableIdDto<long> input);

        Task CreateOrUpdate(SensorInputDto input);

        Task Delete(EntityDto<long> input);
    }
}
