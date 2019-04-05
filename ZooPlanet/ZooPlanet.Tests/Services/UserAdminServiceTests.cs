namespace ZooPlanet.Tests.Services
{
    using ZooPlanet.Data.Models;
    using ZooPlanet.Services.Admin.Implementations;

    using FluentAssertions;
    using System.Threading.Tasks;
    using Xunit;

    public class UserAdminServiceTests
    {
        public UserAdminServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task GetUserByNameAsyncShouldReturnUserByName()
        {
            var db = DbInfrastructure.GetDatabase();

            const string UserName = "Gosho";

            var user = new User
            {
                UserName = UserName,
                Email = "gosho@gosho.com"
            };

            await db.AddAsync(user);
            await db.SaveChangesAsync();

            for (var i = 0; i < 10; i++)
            {
                await db.AddAsync(new User
                {
                    UserName = $"Gosho {i}"
                });
            }

            await db.SaveChangesAsync();

            var userAdminService = new UserAdminService(db);

            var actualUser = await userAdminService.GetUserByNameAsync(UserName);

            user.Should().BeSameAs(actualUser);
        }
    }
}