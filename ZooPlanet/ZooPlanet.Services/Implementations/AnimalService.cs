namespace ZooPlanet.Services.Implementations
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Data;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Models;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.Constants.WebConstants;

    public class AnimalService : IAnimalService
    {
        private readonly ZooPlanetDbContext db;

        public AnimalService(ZooPlanetDbContext db)
        {
            this.db = db;
        }

        public async Task Add(
            string name,
            int age,
            string imageUrl,
            AnimalClass animalClass,
            string userId)
        {
            var animal = new Animal
            {
                Name = name,
                Age = age,
                ImageUrl = imageUrl,
                AnimalClass = animalClass,
                UserId = userId
            };

            await this.db.AddAsync(animal);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<AnimalServiceModel>> All(int page)
            => await this.db
                .Animals
                .OrderBy(a => a.AnimalClass)
                .Skip((page - 1) * AnimalsPerPage)
                .Take(AnimalsPerPage)
                .To<AnimalServiceModel>()
                .ToListAsync();

        public async Task<int> CountAsync()
            => await this.db
                .Animals
                .CountAsync();
    }
}