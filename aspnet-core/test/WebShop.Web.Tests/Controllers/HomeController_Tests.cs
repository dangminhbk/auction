using System.Threading.Tasks;
using WebShop.Models.TokenAuth;
using WebShop.Web.Controllers;
using Shouldly;
using Xunit;

namespace WebShop.Web.Tests.Controllers
{
    public class HomeController_Tests: WebShopWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}