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
using ZeroDemo.AlarmBox.Boxes;
using ZeroDemo.AlarmBox.SensorConfigs.Dtos;
using ZeroDemo.Authorization;

namespace ZeroDemo.AlarmBox.SensorConfigs
{
    [AbpAuthorize(AppPermissions.Pages_SensorConfig)]
    public class SensorConfigAppService : ZeroDemoAppServiceBase, ISensorConfigAppService
    {
        private readonly IRepository<SensorConfig, long> _sensorConfigRepository;
        private readonly IRepository<Box, long> _boxRepository;

        public SensorConfigAppService(IRepository<SensorConfig, long> sensorConfigRepository,
             IRepository<Box, long> boxRepository)
        {
            _sensorConfigRepository = sensorConfigRepository;
            _boxRepository = boxRepository;
        }

        public async Task<PagedResultDto<SensorConfigDto>> GetAll(SensorConfigInputDto input)
        {
            var query = GetFilteredQuery(input);
            var data = await query
                .Include(p => p.Box)
                .Include(p => p.Sensor)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .AsNoTracking().ToListAsync();
            var listDto = ObjectMapper.Map<List<SensorConfigDto>>(data);
            return new PagedResultDto<SensorConfigDto>(await query.CountAsync(), listDto);
        }

        public async Task<SensorConfigDto> GetById(NullableIdDto<long> input)
        {
            var result = new SensorConfigDto();
            if (input.Id.HasValue)
            {
                var data = await _sensorConfigRepository.FirstOrDefaultAsync(input.Id.Value);
                result = ObjectMapper.Map<SensorConfigDto>(data); ;
            }
            return ObjectMapper.Map<SensorConfigDto>(result);
        }

        public async Task CreateOrUpdate(SensorConfigInputDto input)
        {
            if (input.Id.HasValue)
                await Update(input);
            else
                await Create(input);
        }

        [AbpAuthorize(AppPermissions.Pages_SensorConfig_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _sensorConfigRepository.DeleteAsync(input.Id);
        }

        private IQueryable<SensorConfig> GetFilteredQuery(SensorConfigInputDto input)
        {
            var query = _sensorConfigRepository.GetAll()
                .WhereIf(input.BoxId.HasValue, p => p.BoxId == input.BoxId.Value)
                //.WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
                //.WhereIf(input.OnlyLockedUsers, u => u.LockoutEndDateUtc.HasValue && u.LockoutEndDateUtc.Value > DateTime.UtcNow)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), p =>
                    p.Sensor.SensorName.Contains(input.Filter)
                    || p.Id.ToString().Contains(input.Filter)
                    || p.HighValue.ToString().Contains(input.Filter)
                    || p.LowValue.ToString().Contains(input.Filter)
                    || p.TargetValue.ToString().Contains(input.Filter)
                );
            return query;
        }

        [AbpAuthorize(AppPermissions.Pages_SensorConfig_Create)]
        private async Task Create(SensorConfigInputDto input)
        {
            if (await CheckBoxPortInUse(input, false) == true)
            {
                throw new UserFriendlyException(L("PortInUse"));
            }
            if (await CheckMaxBoxPort(input) == true)
            {
                throw new UserFriendlyException(L("OutOfPort"));
            }
            var data = ObjectMapper.Map<SensorConfig>(input);
            await _sensorConfigRepository.InsertAsync(data);
        }

        [AbpAuthorize(AppPermissions.Pages_SensorConfig_Edit)]
        private async Task Update(SensorConfigInputDto input)
        {
            if (await CheckBoxPortInUse(input, true) == true)
            {
                throw new UserFriendlyException(L("PortInUse"));
            }
            var data = await _sensorConfigRepository.FirstOrDefaultAsync(input.Id.Value);
            ObjectMapper.Map(input, data);
        }

        private async Task<bool> CheckBoxPortInUse(SensorConfigInputDto input, bool ignore)
        {
            bool result = false;
            var sensorConfigs = await _sensorConfigRepository.GetAll()
                .Where(p => p.BoxId == input.BoxId)
                .AsNoTracking().ToListAsync();
            if (sensorConfigs.Count == 0)
            {
                return result;
            }
            result = sensorConfigs
                .WhereIf(ignore == true, p => p.Id != input.Id)
                .Any(p => p.BoxPort == input.BoxPort);
            return result;
        }

        private async Task<bool> CheckMaxBoxPort(SensorConfigInputDto input)
        {
            var box = await _boxRepository.GetAll().Where(p => p.Id == input.BoxId)
                .Include(p => p.SensorConfigs).AsNoTracking().FirstOrDefaultAsync();
            if (box.MaxBoxPort > 0 && 
                (box.SensorConfigs == null || box.SensorConfigs.Count == 0 ||
                box.SensorConfigs.Count < box.MaxBoxPort))
                return false;
            else
                return true;
        }
    }
}
