namespace ZooPlanet.Web.Areas.Admin.Models.Contacts
{
    using ZooPlanet.Services.Models;

    using System.Collections.Generic;

    public class ContactsListingViewModel
    {
        public IEnumerable<ContactServiceModel> Contacts { get; set; } = new List<ContactServiceModel>();

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int ContactsCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}
