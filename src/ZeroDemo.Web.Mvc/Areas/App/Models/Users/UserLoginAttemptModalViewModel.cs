using System.Collections.Generic;
using ZeroDemo.Authorization.Users.Dto;

namespace ZeroDemo.Web.Areas.App.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}