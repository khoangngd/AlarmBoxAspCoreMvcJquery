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
using ZeroDemo.AlarmBox.BoxManagers.Dtos;
using ZeroDemo.Authorization;

namespace ZeroDemo.AlarmBox.BoxManagers
{
    [AbpAuthorize(AppPermissions.Pages_BoxManager)]
    public class BoxManagerAppService : ZeroDemoAppServiceBase, IBoxManagerAppService
    {
        private readonly IRepository<BoxManager, long> _boxManagerRepository;
        private readonly IRepository<Box, long> _boxRepository;

        public BoxManagerAppService(IRepository<BoxManager, long> boxManagerRepository,
             IRepository<Box, long> boxRepository)
        {
            _boxManagerRepository = boxManagerRepository;
            _boxRepository = boxRepository;
        }

        public async Task<PagedResultDto<BoxManagerDto>> GetAll(BoxManagerInputDto input)
        {
            var box = await _boxRepository.FirstOrDefaultAsync(input.BoxId.Value);
            var query = GetFilteredQuery(input);
            var data = await query
                .Include(p => p.Box)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .AsNoTracking().ToListAsync();

            var listDto = ObjectMapper.Map<List<BoxManagerDto>>(data);
            var maxBoxManagerPortLeft = box.MaxBoxManagerPort - listDto.Count;
            if (maxBoxManagerPortLeft > 0)
            {
                for (int i = 0; i < maxBoxManagerPortLeft; i++)
                {
                    listDto.Add(new BoxManagerDto());
                    //maxBoxManagerPortLeft--;
                }
            }
            return new PagedResultDto<BoxManagerDto>(await query.CountAsync(), listDto);
        }

        public async Task<BoxManagerDto> GetById(NullableIdDto<long> input)
        {
            var result = new BoxManagerDto();
            if (input.Id.HasValue)
            {
                var data = await _boxManagerRepository.FirstOrDefaultAsync(input.Id.Value);
                result = ObjectMapper.Map<BoxManagerDto>(data); ;
            }
            return ObjectMapper.Map<BoxManagerDto>(result);
        }

        public async Task CreateOrUpdate(BoxManagerInputDto input)
        {
            if (input.Id.HasValue)
                await Update(input);
            else
                await Create(input);
        }

        [AbpAuthorize(AppPermissions.Pages_BoxManager_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _boxManagerRepository.DeleteAsync(input.Id);
        }

        private IQueryable<BoxManager> GetFilteredQuery(BoxManagerInputDto input)
        {
            var query = _boxManagerRepository.GetAll()
                .WhereIf(input.BoxId.HasValue, p => p.BoxId == input.BoxId.Value)
                //.WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
                //.WhereIf(input.OnlyLockedUsers, u => u.LockoutEndDateUtc.HasValue && u.LockoutEndDateUtc.Value > DateTime.UtcNow)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), p =>
                    p.Id.ToString().Contains(input.Filter)
                    || p.ManagerName.ToString().Contains(input.Filter)
                    || p.ManagerEmail.ToString().Contains(input.Filter)
                    || p.ManagerPhoneNumber.ToString().Contains(input.Filter)
                );
            return query;
        }

        [AbpAuthorize(AppPermissions.Pages_BoxManager_Create)]
        private async Task Create(BoxManagerInputDto input)
        {
            if (await CheckManagerPortInUse(input, false) == true)
            {
                throw new UserFriendlyException(L("PortInUse"));
            }
            if (await CheckMaxBoxManagerPort(input) == true)
            {
                throw new UserFriendlyException(L("OutOfPort"));
            }
            var data = ObjectMapper.Map<BoxManager>(input);
            await _boxManagerRepository.InsertAsync(data);
        }

        [AbpAuthorize(AppPermissions.Pages_BoxManager_Edit)]
        private async Task Update(BoxManagerInputDto input)
        {
            if (await CheckManagerPortInUse(input, true) == true)
            {
                throw new UserFriendlyException(L("PortInUse"));
            }
            var data = await _boxManagerRepository.FirstOrDefaultAsync(input.Id.Value);
            ObjectMapper.Map(input, data);
        }

        private async Task<bool> CheckManagerPortInUse(BoxManagerInputDto input, bool ignore)
        {
            bool result = false;
            var boxManagers = await _boxManagerRepository.GetAll()
                .Where(p => p.BoxId == input.BoxId)
                .AsNoTracking().ToListAsync();
            if (boxManagers.Count == 0)
            {
                return result;
            }
            result = boxManagers
                .WhereIf(ignore == true, p => p.Id != input.Id)
                .Any(p => p.ManagerPort == input.ManagerPort);
            return result;
        }

        private async Task<bool> CheckMaxBoxManagerPort(BoxManagerInputDto input)
        {
            var box = await _boxRepository.GetAll().Where(p => p.Id == input.BoxId)
                .Include(p => p.BoxManagers).AsNoTracking().FirstOrDefaultAsync();
            if (box.MaxBoxManagerPort > 0 &&
                (box.BoxManagers == null || box.BoxManagers.Count == 0 ||
                box.BoxManagers.Count < box.MaxBoxManagerPort))
                return false;
            else
                return true;
        }
    }
}
