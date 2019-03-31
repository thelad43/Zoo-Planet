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

        public async Task<IEnumerable<ContactServiceModel>> All(int page)
            => await this.db
                .Contacts
                .OrderByDescending(c => c.AddedOn)
                .Skip((page - 1) * ContactsPerPage)
                .Take(ContactsPerPage)
                .To<ContactServiceModel>()
                .ToListAsync();

        public async Task<Contact> ById(int id)
            => await this.db
                .Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<int> CountAsync()
            => await this.db
                .Contacts
                .CountAsync();

        public async Task Delete(int id)
        {
            var contact = await this.ById(id);

            this.db.Remove(contact);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string title, string message, ContactType contactType)
        {
            var contact = await this.ById(id);

            contact.Title = title;
            contact.Message = message;
            contact.ContactType = contactType;

            await this.db.SaveChangesAsync();
        }
    }
}