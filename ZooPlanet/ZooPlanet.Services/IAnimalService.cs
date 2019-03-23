namespace ZooPlanet.Services
{
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IAnimalService
    {
        Task<IEnumerable<AnimalServiceModel>> All(int page);

        Task Add(string name, int age, string imageUrl, AnimalClass animalClass, string userId);

        Task<int> CountAsync();
    }
}