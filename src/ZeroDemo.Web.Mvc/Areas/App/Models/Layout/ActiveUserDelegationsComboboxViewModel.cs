using System.Collections.Generic;
using ZeroDemo.Authorization.Delegation;
using ZeroDemo.Authorization.Users.Delegation.Dto;

namespace ZeroDemo.Web.Areas.App.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }
        
        public List<UserDelegationDto> UserDelegations { get; set; }
    }
}
