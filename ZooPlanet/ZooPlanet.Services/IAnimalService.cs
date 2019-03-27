namespace ZooPlanet.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Models;

    public interface IAnimalService
    {
        Task<IEnumerable<AnimalServiceModel>> All(int page);

        Task Add(string name, int age, string imageUrl, AnimalClass animalClass, string userId);

        Task<int> CountAsync();

        Task<Animal> ById(int id);

        Task<Animal> ByUser(int id, User user);

        Task Edit(int id, string name, int age, AnimalClass animalClass, string imageUrl);

        Task Delete(int id);
    }
}