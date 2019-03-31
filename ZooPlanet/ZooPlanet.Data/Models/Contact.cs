namespace ZooPlanet.Data.Models
{
    using ZooPlanet.Data.Models.Enums;

    using System.ComponentModel.DataAnnotations;

    using static ZooPlanet.Common.Constants.DataConstants;
    using static ZooPlanet.Common.Constants.ErrorMessages;

    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthErrorMessage)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MinLength(MessageMinLength, ErrorMessage = MessageMinLengthErrorMessage)]
        [MaxLength(MessageMaxLength, ErrorMessage = MessageMaxLengthErrorMessage)]
        public string Message { get; set; }

        public ContactType ContactType { get; set; }

        public bool IsAnswered { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}