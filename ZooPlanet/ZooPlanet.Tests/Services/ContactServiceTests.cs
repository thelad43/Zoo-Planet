namespace ZooPlanet.Tests.Services
{
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Implementations;

    using FluentAssertions;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ContactServiceTests
    {
        public ContactServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AddAsyncShouldAddContactToDb()
        {
            var db = DbInfrastructure.GetDatabase();

            var contactService = new ContactService(db);

            const string Title = "Some title";
            const string Message = "Are there tigers in your zoo?";
            const string UserId = "some_user_id";

            await contactService.AddAsync(Title, Message, ContactType.Message, UserId);

            var actual = db.Contacts.FirstOrDefault();

            actual.Title.Should().Be(Title);

            actual.Message.Should().Be(Message);

            actual.ContactType.Should().Be(ContactType.Message);

            actual.UserId.Should().Be(UserId);
        }
    }
}