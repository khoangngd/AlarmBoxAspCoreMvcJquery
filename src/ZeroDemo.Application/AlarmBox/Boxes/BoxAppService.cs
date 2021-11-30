using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using ZeroDemo.AlarmBox.Boxes.Dtos;
using ZeroDemo.Authorization;

namespace ZeroDemo.AlarmBox.Boxes
{
    [AbpAuthorize(AppPermissions.Pages_Box)]
    public class BoxAppService : ZeroDemoAppServiceBase, IBoxAppService
    {
        private readonly IRepository<Box, long> _boxRepository;

        public BoxAppService(IRepository<Box, long> boxRepository)
        {
            _boxRepository = boxRepository;
        }

        public async Task<PagedResultDto<BoxDto>> GetAll(BoxInputDto input)
        {
            var query = GetFilteredQuery(input);
            var data = await query.OrderBy(input.Sorting).PageBy(input).AsNoTracking().ToListAsync();
            var listDto = ObjectMapper.Map<List<BoxDto>>(data);
            return new PagedResultDto<BoxDto>(await query.CountAsync(), listDto);
        }

        public async Task<BoxDto> GetById(NullableIdDto<long> input)
        {
            var result = new BoxDto();
            if (input.Id.HasValue)
            {
                var data = await _boxRepository.FirstOrDefaultAsync(input.Id.Value);
                result = ObjectMapper.Map<BoxDto>(data); ;
            }            
            return ObjectMapper.Map<BoxDto>(result);
        }

        public async Task CreateOrUpdate(BoxInputDto input)
        {
            if (input.Id.HasValue)
                await Update(input);
            else
                await Create(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Box_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _boxRepository.DeleteAsync(input.Id);
        }

        private IQueryable<Box> GetFilteredQuery(BoxInputDto input)
        {
            var query = _boxRepository.GetAll()
                //.WhereIf(input.Role.HasValue, u => u.Roles.Any(r => r.RoleId == input.Role.Value))
                //.WhereIf(input.OnlyLockedUsers, u => u.LockoutEndDateUtc.HasValue && u.LockoutEndDateUtc.Value > DateTime.UtcNow)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    p => p.BoxName.Contains(input.Filter) || p.Location.Contains(input.Filter)
                )
                .WhereIf(input.IsDeletedBox.HasValue && input.IsDeletedBox == true,
                    p => p.IsDeleted == true
                )
                ;
            return query;
        }

        [AbpAuthorize(AppPermissions.Pages_Box_Create)]
        private async Task Create(BoxInputDto input)
        {
            var data = ObjectMapper.Map<Box>(input);
            await _boxRepository.InsertAsync(data);
        }

        [AbpAuthorize(AppPermissions.Pages_Box_Edit)]
        private async Task Update(BoxInputDto input)
        {
            var data = await _boxRepository.FirstOrDefaultAsync(input.Id.Value);
            ObjectMapper.Map(input, data);
        }


    }
}
