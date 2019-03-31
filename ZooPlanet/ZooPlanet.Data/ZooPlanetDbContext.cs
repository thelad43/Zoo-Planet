namespace ZooPlanet.Data
{
    using ZooPlanet.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ZooPlanetDbContext : IdentityDbContext<User>
    {
        public ZooPlanetDbContext(DbContextOptions<ZooPlanetDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.Animals)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder
                .Entity<User>()
                .HasMany(u => u.Contacts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            base.OnModelCreating(builder);
        }
    }
}