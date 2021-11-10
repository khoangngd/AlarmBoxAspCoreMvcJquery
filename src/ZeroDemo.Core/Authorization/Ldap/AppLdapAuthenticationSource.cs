using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using ZeroDemo.Authorization.Users;
using ZeroDemo.MultiTenancy;

namespace ZeroDemo.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}