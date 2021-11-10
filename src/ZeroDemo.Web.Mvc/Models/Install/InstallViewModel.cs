using System.Collections.Generic;
using Abp.Localization;
using ZeroDemo.Install.Dto;

namespace ZeroDemo.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
