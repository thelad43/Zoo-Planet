namespace ZooPlanet.Web.Areas.Admin.Models.Contacts
{
    using ZooPlanet.Web.Models;

    using System.ComponentModel.DataAnnotations;

    public class ContactAdminViewModel : ContactViewModel
    {
        [Display(Name = "Is Answered")]
        public bool IsAnswered { get; set; }
    }
}
