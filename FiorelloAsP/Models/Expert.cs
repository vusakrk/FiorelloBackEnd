using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }
    }
}
