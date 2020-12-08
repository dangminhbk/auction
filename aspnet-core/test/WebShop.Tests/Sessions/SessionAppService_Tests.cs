using Shouldly;
using System.Threading.Tasks;
using WebShop.Sessions;
using Xunit;

namespace WebShop.Tests.Sessions
{
    public class SessionAppService_Tests : WebShopTestBase
    {
        private readonly ISessionAppService _sessionAppService;

        public SessionAppService_Tests()
        {
            _sessionAppService = Resolve<ISessionAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_Current_User_When_Logged_In_As_Host()
        {
            // Arrange
            LoginAsHostAdmin();

            // Act
            WebShop.Sessions.Dto.GetCurrentLoginInformationsOutput output = await _sessionAppService.GetCurrentLoginInformations();

            // Assert
            Authorization.Users.User currentUser = await GetCurrentUserAsync();
            output.User.ShouldNotBe(null);
            output.User.Name.ShouldBe(currentUser.Name);
            output.User.Surname.ShouldBe(currentUser.Surname);

            output.Tenant.ShouldBe(null);
        }

        [Fact]
        public async Task Should_Get_Current_User_And_Tenant_When_Logged_In_As_Tenant()
        {
            // Act
            WebShop.Sessions.Dto.GetCurrentLoginInformationsOutput output = await _sessionAppService.GetCurrentLoginInformations();

            // Assert
            Authorization.Users.User currentUser = await GetCurrentUserAsync();
            MultiTenancy.Tenant currentTenant = await GetCurrentTenantAsync();

            output.User.ShouldNotBe(null);
            output.User.Name.ShouldBe(currentUser.Name);

            output.Tenant.ShouldNotBe(null);
            output.Tenant.Name.ShouldBe(currentTenant.Name);
        }
    }
}
