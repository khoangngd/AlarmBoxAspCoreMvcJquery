using System.Collections.Generic;
using ZeroDemo.Authorization.Users.Importing.Dto;
using ZeroDemo.Dto;

namespace ZeroDemo.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
