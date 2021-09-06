using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public bool HasDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
