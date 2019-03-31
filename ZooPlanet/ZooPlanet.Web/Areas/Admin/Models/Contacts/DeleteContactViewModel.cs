namespace ZooPlanet.Web.Areas.Admin.Models.Contacts
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Data.Models;

    public class DeleteContactViewModel : IMapFrom<Contact>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}