namespace ZooPlanet.Services.Admin.Implementations
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

    using static ZooPlanet.Common.Constants.WebConstants;

    public class ContactAdminService : IContactAdminService
    {
        private readonly ZooPlanetDbContext db;

        public ContactAdminService(ZooPlanetDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ContactServiceModel>> AllAsync(int page, ContactFilter filter)
        {
            var query = this.db
                .Contacts
                .AsQueryable();

            if (filter == ContactFilter.All)
            {
                return await query
                    .OrderByDescending(c => c.AddedOn)
                    .Skip((page - 1) * ContactsPerPage)
                    .Take(ContactsPerPage)
                    .To<ContactServiceModel>()
                    .ToListAsync();
            }

            return await this.db
                .Contacts
                .Where(c => c.IsAnswered == (filter == ContactFilter.Answered))
                .OrderByDescending(c => c.AddedOn)
                .Skip((page - 1) * ContactsPerPage)
                .Take(ContactsPerPage)
                .To<ContactServiceModel>()
                .ToListAsync();
        }

        public async Task<Contact> ByIdAsync(int id)
            => await this.db
                .Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<int> CountAsync()
            => await this.db
                .Contacts
                .CountAsync();

        public async Task DeleteAsync(int id)
        {
            var contact = await this.ByIdAsync(id);

            this.db.Remove(contact);
            await this.db.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string message, ContactType contactType, bool isAnswered)
        {
            var contact = await this.ByIdAsync(id);

            contact.Title = title;
            contact.Message = message;
            contact.ContactType = contactType;
            contact.IsAnswered = isAnswered;

            await this.db.SaveChangesAsync();
        }
    }
}