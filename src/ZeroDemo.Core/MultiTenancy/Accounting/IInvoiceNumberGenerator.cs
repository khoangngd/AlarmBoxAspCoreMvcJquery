using System.Threading.Tasks;
using Abp.Dependency;

namespace ZeroDemo.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}