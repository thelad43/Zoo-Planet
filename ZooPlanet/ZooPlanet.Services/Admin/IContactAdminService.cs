namespace ZooPlanet.Services.Admin
{
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContactAdminService
    {
        Task<IEnumerable<ContactServiceModel>> AllAsync(int page, ContactFilter filter);

        Task<int> CountAsync();

        Task<Contact> ByIdAsync(int id);

        Task DeleteAsync(int id);

        Task EditAsync(int id, string title, string message, ContactType contactType, bool isAnswered);
    }
}