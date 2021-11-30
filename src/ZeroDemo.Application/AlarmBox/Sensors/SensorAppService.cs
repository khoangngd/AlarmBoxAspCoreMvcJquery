using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Sensors.Dtos;
using ZeroDemo.Authorization;

namespace ZeroDemo.AlarmBox.Sensors
{
    [AbpAuthorize(AppPermissions.Pages_Sensor)]
    public class SensorAppService : ZeroDemoAppServiceBase, ISensorAppService
    {
        private readonly IRepository<Sensor, long> _sensorRepository;

        public SensorAppService(IRepository<Sensor, long> sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task<PagedResultDto<SensorDto>> GetAll(SensorInputDto input)
        {
            var query = GetFilteredQuery(input);
            var data = await query
                .Include(p => p.SensorConfigs) //lỗi map
                .OrderBy(input.Sorting).PageBy(input)
                .AsNoTracking().ToListAsync();
            var listDto = ObjectMapper.Map<List<SensorDto>>(data);
            return new PagedResultDto<SensorDto>(await query.CountAsync(), listDto);
        }

        public async Task<SensorDto> GetById(NullableIdDto<long> input)
        {
            var result = new SensorDto();
            if (input.Id.HasValue)
            {
                var data = await _sensorRepository.FirstOrDefaultAsync(input.Id.Value);
                result = ObjectMapper.Map<SensorDto>(data); ;
            }
            return ObjectMapper.Map<SensorDto>(result);
        }

        public async Task CreateOrUpdate(SensorInputDto input)
        {
            if (input.Id.HasValue)
                await Update(input);
            else
                await Create(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Sensor_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            var sensor = await _sensorRepository.GetAll()
                .Include(p => p.SensorConfigs)
                .Where(p => p.Id == input.Id)
                .AsNoTracking().FirstOrDefaultAsync();
            var countSensorConfig = sensor.SensorConfigs.Count();
            if (countSensorConfig == 0)
                throw new UserFriendlyException(L("SensorInUse"));
            await _sensorRepository.DeleteAsync(input.Id);
        }

        private IQueryable<Sensor> GetFilteredQuery(SensorInputDto input)
        {
            var query = _sensorRepository.GetAll()
                //.WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
                //.WhereIf(input.OnlyLockedUsers, u => u.LockoutEndDateUtc.HasValue && u.LockoutEndDateUtc.Value > DateTime.UtcNow)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), p =>
                    p.SensorName.Contains(input.Filter)
                    || p.Id.ToString().Contains(input.Filter)
                    || p.HighValueDefault.ToString().Contains(input.Filter)
                    || p.LowValueDefault.ToString().Contains(input.Filter)
                    || p.TargetValueDefault.ToString().Contains(input.Filter)
                );
            return query;
        }

        [AbpAuthorize(AppPermissions.Pages_Sensor_Create)]
        private async Task Create(SensorInputDto input)
        {
            var data = ObjectMapper.Map<Sensor>(input);
            await _sensorRepository.InsertAsync(data);
        }

        [AbpAuthorize(AppPermissions.Pages_Sensor_Edit)]
        private async Task Update(SensorInputDto input)
        {
            var data = await _sensorRepository.FirstOrDefaultAsync(input.Id.Value);
            ObjectMapper.Map(input, data);
        }


    }
}
