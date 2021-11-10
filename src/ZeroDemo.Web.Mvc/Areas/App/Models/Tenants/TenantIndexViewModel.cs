using System.Collections.Generic;
using ZeroDemo.Editions.Dto;

namespace ZeroDemo.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}