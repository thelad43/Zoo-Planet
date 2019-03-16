namespace ZooPlanet.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public List<Animal> Animals { get; set; }
    }
}