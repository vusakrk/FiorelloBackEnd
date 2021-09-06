using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Models
{
    public class SliderContent
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
