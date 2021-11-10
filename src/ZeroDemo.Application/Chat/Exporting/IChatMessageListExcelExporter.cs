using System.Collections.Generic;
using Abp;
using ZeroDemo.Chat.Dto;
using ZeroDemo.Dto;

namespace ZeroDemo.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
