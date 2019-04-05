namespace ZooPlanet.Tests.Services
{
    using ZooPlanet.Data;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Admin.Implementations;
    using ZooPlanet.Services.Models;

    using FluentAssertions;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    using static ZooPlanet.Common.Constants.WebConstants;

    public class ContactAdminServiceTests
    {
        private readonly Random randomGenerator = new Random();

        public ContactAdminServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncWithoutFilterShouldReturnAllContactsByPages()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.Add100RandomAnsweredContacts(db);

            await db.SaveChangesAsync();

            var contactAdminService = new ContactAdminService(db);

            for (var i = 0; i < 10; i++)
            {
                var contacts = await contactAdminService.AllAsync(i + 1, ContactFilter.All);

                contacts.Should().HaveCount(ContactsPerPage);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectContactsByAnsweredFilter()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.Add100AnsweredContacts(db);

            await db.SaveChangesAsync();

            var contactAdminService = new ContactAdminService(db);

            for (var i = 0; i < 10; i++)
            {
                var contacts = await contactAdminService.AllAsync(i + 1, ContactFilter.All);

                contacts.Should().HaveCount(ContactsPerPage);

                foreach (var contact in contacts)
                {
                    contact
                        .Should()
                        .Match<ContactServiceModel>(c => c.IsAnswered == true);
                }
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectContactsByUnansweredFilter()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.Add100UnansweredContacts(db);

            await db.SaveChangesAsync();

            var contactAdminService = new ContactAdminService(db);

            for (var i = 0; i < 10; i++)
            {
                var contacts = await contactAdminService.AllAsync(i + 1, ContactFilter.All);

                contacts.Should().HaveCount(ContactsPerPage);

                foreach (var contact in contacts)
                {
                    contact
                        .Should()
                        .Match<ContactServiceModel>(c => c.IsAnswered == false);
                }
            }
        }

        [Fact]
        public async Task CountAsyncShoulReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.Add100AnsweredContacts(db);
            await this.Add100UnansweredContacts(db);
            await this.Add100RandomAnsweredContacts(db);

            var contactAdminService = new ContactAdminService(db);

            var count = await contactAdminService.CountAsync();

            count.Should().Be(300);
        }

        [Fact]
        public async Task ByIdAsyncShouldReturnContactById()
        {
            var db = DbInfrastructure.GetDatabase();

            const string Title = "Title";
            const string Message = "Message";

            var contact = new Contact
            {
                Title = Title,
                Message = Message
            };

            await db.AddAsync(contact);
            await db.SaveChangesAsync();

            var contactAdminService = new ContactAdminService(db);

            var actualContact = await contactAdminService.ByIdAsync(contact.Id);

            actualContact.Title.Should().Be(Title);

            actualContact.Message.Should().Be(Message);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteContact()
        {
            var db = DbInfrastructure.GetDatabase();

            var contact = new Contact
            {
                Title = "Title",
                Message = "Message"
            };

            await db.AddAsync(contact);
            await db.SaveChangesAsync();

            var contactAdminService = new ContactAdminService(db);

            await contactAdminService.DeleteAsync(contact.Id);

            var count = await contactAdminService.CountAsync();

            count.Should().Be(0);
        }

        [Fact]
        public async Task EditAsyncShouldEditContact()
        {
            var db = DbInfrastructure.GetDatabase();

            var contact = new Contact
            {
                Title = "Title",
                Message = "Message",
                ContactType = ContactType.Bug,
                IsAnswered = false
            };

            await db.AddAsync(contact);
            await db.SaveChangesAsync();

            var contactAdminService = new ContactAdminService(db);

            const string Title = "Edited Title";
            const string Message = "Edited Message";

            await contactAdminService.EditAsync(contact.Id, Title, Message, ContactType.Question, true);

            var actualContact = await contactAdminService.ByIdAsync(contact.Id);

            actualContact.Title.Should().Be(Title);

            actualContact.Message.Should().Be(Message);

            actualContact.ContactType.Should().Be(ContactType.Question);

            actualContact.IsAnswered.Should().BeTrue();
        }

        private async Task Add100RandomAnsweredContacts(ZooPlanetDbContext db)
        {
            for (var i = 0; i < 100; i++)
            {
                await db.AddAsync(new Contact
                {
                    Title = $"Random title {i}",
                    AddedOn = DateTime.UtcNow,
                    Message = $"Some Random Message {i}",
                    IsAnswered = this.randomGenerator.Next(0, 2) == 1,
                    ContactType = (ContactType)this.randomGenerator.Next(1, 4)
                });
            }

            await db.SaveChangesAsync();
        }

        private async Task Add100UnansweredContacts(ZooPlanetDbContext db)
        {
            for (var i = 0; i < 100; i++)
            {
                await db.AddAsync(new Contact
                {
                    Title = $"Random title {i}",
                    AddedOn = DateTime.UtcNow,
                    Message = $"Some Random Message {i}",
                    IsAnswered = false,
                    ContactType = (ContactType)this.randomGenerator.Next(1, 4)
                });
            }

            await db.SaveChangesAsync();
        }

        private async Task Add100AnsweredContacts(ZooPlanetDbContext db)
        {
            for (var i = 0; i < 100; i++)
            {
                await db.AddAsync(new Contact
                {
                    Title = $"Random title {i}",
                    AddedOn = DateTime.UtcNow,
                    Message = $"Some Random Message {i}",
                    IsAnswered = true,
                    ContactType = (ContactType)this.randomGenerator.Next(1, 4)
                });
            }

            await db.SaveChangesAsync();
        }
    }
}