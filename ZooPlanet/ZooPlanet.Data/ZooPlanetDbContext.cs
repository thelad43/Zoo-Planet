namespace ZooPlanet.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ZooPlanetDbContext : IdentityDbContext
    {
        public ZooPlanetDbContext(DbContextOptions<ZooPlanetDbContext> options)
            : base(options)
        {
        }
    }
}