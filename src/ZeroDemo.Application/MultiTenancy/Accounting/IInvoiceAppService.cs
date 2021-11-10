using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using ZeroDemo.MultiTenancy.Accounting.Dto;

namespace ZeroDemo.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
