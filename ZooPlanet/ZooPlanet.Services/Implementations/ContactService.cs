namespace ZooPlanet.Services.Implementations
{
    using ZooPlanet.Data;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;

    using System;
    using System.Threading.Tasks;

    public class ContactService : IContactService
    {
        private readonly ZooPlanetDbContext db;

        public ContactService(ZooPlanetDbContext db)
        {
            this.db = db;
        }

        public async Task Add(string title, string message, ContactType contactType, string userId)
        {
            var contact = new Contact
            {
                Title = title,
                Message = message,
                ContactType = contactType,
                AddedOn = DateTime.UtcNow,
                UserId = userId
            };

            await this.db.AddAsync(contact);
            await this.db.SaveChangesAsync();
        }
    }
}