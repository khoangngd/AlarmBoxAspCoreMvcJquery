using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using ZeroDemo.Dto;

namespace ZeroDemo.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
