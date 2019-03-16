namespace ZooPlanet.Services.Admin.Implementations
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Data;
    using ZooPlanet.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using System.Linq;
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