using System.Collections.Generic;
using ZeroDemo.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace ZeroDemo.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
