using Abp.Authorization;
using WebShop.Authorization.Roles;
using WebShop.Authorization.Users;

namespace WebShop.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
