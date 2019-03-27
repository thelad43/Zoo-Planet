namespace ZooPlanet.Services.Models
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;

    public class AnimalServiceModel : IMapFrom<Animal>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public AnimalClass AnimalClass { get; set; }

        public string ImageUrl { get; set; }
    }
}