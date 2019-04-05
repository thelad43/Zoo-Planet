namespace ZooPlanet.Services
{
    using ZooPlanet.Data.Models.Enums;

    using System.Threading.Tasks;

    public interface IContactService
    {
        Task AddAsync(string title, string message, ContactType contactType, string userId);
    }
}