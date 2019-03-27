namespace ZooPlanet.Web.Models.Animals
{
    using System.Collections.Generic;
    using ZooPlanet.Services.Models;

    public class AnimalsListingViewModel
    {
        public IEnumerable<AnimalServiceModel> Animals { get; set; } = new List<AnimalServiceModel>();

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int AnimalsCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}