namespace ZooPlanet.Services.Admin.Implementations
{
    using ZooPlanet.Data;
    using ZooPlanet.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class UserAdminService : IUserAdminService
    {
        private readonly ZooPlanetDbContext db;

        public UserAdminService(ZooPlanetDbContext db)
        {
            this.db = db;
        }

        public async Task<User> GetUserByNameAsync(string userName)
            => await this.db
                .Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}