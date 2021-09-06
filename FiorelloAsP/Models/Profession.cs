using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Models
{
    public class Profession
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Expert> Experts { get; set; }
    }
}
