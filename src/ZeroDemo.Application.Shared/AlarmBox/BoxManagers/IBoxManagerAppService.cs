using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.BoxManagers.Dtos;

namespace ZeroDemo.AlarmBox.BoxManagers
{
    public interface IBoxManagerAppService : IApplicationService
    {
        Task<PagedResultDto<BoxManagerDto>> GetAll(BoxManagerInputDto input);

        Task<BoxManagerDto> GetById(NullableIdDto<long> input);

        Task CreateOrUpdate(BoxManagerInputDto input);

        Task Delete(EntityDto<long> input);
    }
}
