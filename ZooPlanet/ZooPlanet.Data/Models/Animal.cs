namespace ZooPlanet.Data.Models
{
    using ZooPlanet.Data.Models.Enums;

    using System.ComponentModel.DataAnnotations;

    public class Animal
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public AnimalClass AnimalClass { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}