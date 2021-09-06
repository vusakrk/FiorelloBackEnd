using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [StringLength(40)]
        public string Title { get; set; }
        [StringLength(80)]
        public string Description { get; set; }
        [Required]
        public string PostedDate { get; set; }
    }
}
