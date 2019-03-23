namespace ZooPlanet.Services
{
    using ZooPlanet.Data.Models.Enums;

    using System.Threading.Tasks;

    public interface IAnimalService
    {
        Task Add(string name, int age, string imageUrl, AnimalClass animalClass, string userId);
    }
}