namespace ZooPlanet.Services.Implementations
{
    using ZooPlanet.Data;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;

    using System.Threading.Tasks;

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
    }
}