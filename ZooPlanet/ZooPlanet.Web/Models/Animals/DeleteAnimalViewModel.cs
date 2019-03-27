namespace ZooPlanet.Web.Models.Animals
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Data.Models;

    public class DeleteAnimalViewModel : IMapFrom<Animal>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}