using System.ComponentModel.DataAnnotations;

namespace ZeroDemo.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}