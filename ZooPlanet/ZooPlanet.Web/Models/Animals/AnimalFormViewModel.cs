namespace ZooPlanet.Web.Models.Animals
{
    using System.ComponentModel.DataAnnotations;
    using ZooPlanet.Data.Models.Enums;
    using static Common.Constants.DataConstants;
    using static Common.Constants.ErrorMessages;

    public class AnimalFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(AnimalNameMinLength, ErrorMessage = AnimalNameMinLengthErrorMessage)]
        [MaxLength(AnimalNameMaxLength, ErrorMessage = AnimalNameMaxLengthErrorMessage)]
        public string Name { get; set; }

        [Range(AnimalMinAge, AnimalMaxAge, ErrorMessage = AnimalAgeErrorMessage)]
        public int Age { get; set; }

        [Url]
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Animal Class")]
        public AnimalClass AnimalClass { get; set; }
    }
}