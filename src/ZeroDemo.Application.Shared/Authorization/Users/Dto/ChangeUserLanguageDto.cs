using System.ComponentModel.DataAnnotations;

namespace ZeroDemo.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
