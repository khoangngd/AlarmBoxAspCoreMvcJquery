using System.Collections.Generic;
using Abp.Collections.Extensions;
using Abp.Dependency;
using ZeroDemo.Authorization.Users.Importing.Dto;
using ZeroDemo.DataExporting.Excel.NPOI;
using ZeroDemo.Dto;
using ZeroDemo.Storage;

namespace ZeroDemo.Authorization.Users.Importing
{
    public class InvalidUserExporter : NpoiExcelExporterBase, IInvalidUserExporter, ITransientDependency
    {
        public InvalidUserExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportUserDto> userListDtos)
        {
            return CreateExcelPackage(
                "InvalidUserImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(L("InvalidUserImports"));
                    
                    AddHeader(
                        sheet,
                        L("UserName"),
                        L("Name"),
                        L("Surname"),
                        L("EmailAddress"),
                        L("PhoneNumber"),
                        L("Password"),
                        L("Roles"),
                        L("Refuse Reason")
                    );

                    AddObjects(
                        sheet, 2, userListDtos,
                        _ => _.UserName,
                        _ => _.Name,
                        _ => _.Surname,
                        _ => _.EmailAddress,
                        _ => _.PhoneNumber,
                        _ => _.Password,
                        _ => _.AssignedRoleNames?.JoinAsString(","),
                        _ => _.Exception
                    );

                    for (var i = 0; i < 8; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                });
        }
    }
}
