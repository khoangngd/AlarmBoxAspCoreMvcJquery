using System.Collections.Generic;
using ZeroDemo.Caching.Dto;

namespace ZeroDemo.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}