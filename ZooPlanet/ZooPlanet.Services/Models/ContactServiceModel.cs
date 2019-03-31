namespace ZooPlanet.Services.Models
{
    using ZooPlanet.Common.Mapping;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;

    using AutoMapper;

    public class ContactServiceModel : IMapFrom<Contact>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public ContactType ContactType { get; set; }

        public bool IsAnswered { get; set; }

        public string User { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
            => configuration.CreateMap<Contact, ContactServiceModel>()
                .ForMember(src => src.User, cfg => cfg.MapFrom(dest => dest.User.UserName));
    }
}
