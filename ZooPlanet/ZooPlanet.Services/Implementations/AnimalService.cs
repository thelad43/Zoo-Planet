namespace ZooPlanet.Services.Implementations
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Data;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Models;

    using Microsoft.EntityFrameworkCore;
    using System;
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

        public async Task AddAsync(
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

        public async Task<IEnumerable<AnimalServiceModel>> AllAsync(int page)
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

        public async Task DeleteAsync(int id)
        {
            var animal = await this.db
                .Animals
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                throw new InvalidOperationException();
            }

            this.db.Remove(animal);

            await this.db.SaveChangesAsync();
        }

        public async Task EditAsync(
            int id,
            string name,
            int age,
            AnimalClass animalClass,
            string imageUrl)
        {
            var animal = await this.db
                .Animals
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                throw new InvalidOperationException();
            }

            animal.Name = name;
            animal.Age = age;
            animal.ImageUrl = imageUrl;
            animal.AnimalClass = animalClass;

            await this.db.SaveChangesAsync();
        }

        public async Task<Animal> ByIdAsync(int id)
            => await this.db
                .Animals
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Animal> ByUserAsync(int id, User user)
            => await this.db
                .Animals
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == user.Id);
    }
}