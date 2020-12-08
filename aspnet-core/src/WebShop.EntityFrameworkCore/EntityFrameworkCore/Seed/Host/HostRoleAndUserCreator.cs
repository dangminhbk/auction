using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using WebShop.Authorization;
using WebShop.Authorization.Roles;
using WebShop.Authorization.Users;

namespace WebShop.EntityFrameworkCore.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly WebShopDbContext _context;

        public HostRoleAndUserCreator(WebShopDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
            AddPermissionToBuyer();
            AddPermissionToSeller();
        }

        private Role GetStaticRole(string roleName)
        {
            Role role = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == roleName);
            if (role == null)
            {
                role = _context.Roles.Add(new Role(null, roleName, roleName) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            return role;
        }
        private void AddPermissionToBuyer()
        {
            Role buyerRole = GetStaticRole(StaticRoleNames.Host.Buyer);
            System.Collections.Generic.List<string> grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == buyerRole.Id)
                .Select(p => p.Name)
                .ToList();

            if (!grantedPermissions.Contains(PermissionNames.Buyers))
            {
                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = PermissionNames.Buyers,
                        IsGranted = true,
                        RoleId = buyerRole.Id
                    });
            }

            _context.SaveChangesAsync();
        }

        private void AddPermissionToSeller()
        {
            Role BuyerRole = GetStaticRole(StaticRoleNames.Host.Seller);
            System.Collections.Generic.List<string> grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == BuyerRole.Id)
                .Select(p => p.Name)
                .ToList();

            if (!grantedPermissions.Contains(PermissionNames.Sellers))
            {
                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = PermissionNames.Sellers,
                        IsGranted = true,
                        RoleId = BuyerRole.Id
                    });
            }

            _context.SaveChangesAsync();
        }

        private void CreateHostRoleAndUsers()
        {
            // Admin role for host

            Role adminRoleForHost = GetStaticRole(StaticRoleNames.Host.Admin);

            // Grant all permissions to admin role for host

            System.Collections.Generic.List<string> grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == adminRoleForHost.Id)
                .Select(p => p.Name)
                .ToList();

            System.Collections.Generic.List<Permission> permissions = PermissionFinder
                .GetAllPermissions(new WebShopAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRoleForHost.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user for host

            User adminUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);
            if (adminUserForHost == null)
            {
                User user = new User
                {
                    TenantId = null,
                    UserName = AbpUserBase.AdminUserName,
                    Name = "admin",
                    Surname = "admin",
                    EmailAddress = "admin@aspnetboilerplate.com",
                    IsEmailConfirmed = true,
                    IsActive = true
                };

                user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, "123qwe");
                user.SetNormalizedNames();

                adminUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                _context.SaveChanges();
            }
        }
    }
}
