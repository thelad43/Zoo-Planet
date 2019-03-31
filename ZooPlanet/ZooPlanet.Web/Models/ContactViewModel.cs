namespace ZooPlanet.Web.Models
{
    using ZooPlanet.Data.Models.Enums;

    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.DataConstants;
    using static Common.Constants.ErrorMessages;

    public class ContactViewModel
    {
        [Required]
        [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthErrorMessage)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MinLength(MessageMinLength, ErrorMessage = MessageMinLengthErrorMessage)]
        [MaxLength(MessageMaxLength, ErrorMessage = MessageMaxLengthErrorMessage)]
        public string Message { get; set; }

        [Display(Name = "Contact Type")]
        public ContactType ContactType { get; set; }
    }
}
