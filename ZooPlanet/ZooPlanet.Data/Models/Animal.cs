namespace ZooPlanet.Data.Models
{
    using ZooPlanet.Data.Models.Enums;

    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.DataConstants;
    using static Common.Constants.ErrorMessages;

    public class Animal
    {
        public int Id { get; set; }

        [Required]
        [MinLength(AnimalNameMinLength, ErrorMessage = AnimalNameMinLengthErrorMessage)]
        [MaxLength(AnimalNameMaxLength, ErrorMessage = AnimalNameMaxLengthErrorMessage)]
        public string Name { get; set; }

        [Range(AnimalMinAge, AnimalMaxAge, ErrorMessage = AnimalAgeErrorMessage)]
        public int Age { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public AnimalClass AnimalClass { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}