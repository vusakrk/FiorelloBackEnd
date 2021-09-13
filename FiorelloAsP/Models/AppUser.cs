using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FiorelloAsP.Models
{
    public class AppUser:IdentityUser
    {
        [Required,StringLength(100)]
        public string FullName { get; set; }
        public bool Status { get; set; }

    }
}
