namespace ZooPlanet.Services.Admin
{
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContactAdminService
    {
        Task<IEnumerable<ContactServiceModel>> All(int page);

        Task<int> CountAsync();

        Task<Contact> ById(int id);

        Task Delete(int id);

        Task Edit(int id, string title, string message, ContactType contactType, bool isAnswered);
    }
}