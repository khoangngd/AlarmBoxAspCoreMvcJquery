using System.Collections.Generic;
using ZeroDemo.Authorization.Users.Dto;
using ZeroDemo.Dto;

namespace ZeroDemo.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}