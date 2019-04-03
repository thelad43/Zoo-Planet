namespace ZooPlanet.Services
{
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAnimalService
    {
        Task<IEnumerable<AnimalServiceModel>> AllAsync(int page);

        Task AddAsync(string name, int age, string imageUrl, AnimalClass animalClass, string userId);

        Task<int> CountAsync();

        Task<Animal> ByIdAsync(int id);

        Task<Animal> ByUserAsync(int id, User user);

        Task EditAsync(int id, string name, int age, AnimalClass animalClass, string imageUrl);

        Task DeleteAsync(int id);
    }
}