using FiorelloAsP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderContent SliderContent { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public About About { get; set; }
        public List<AboutInfo> AboutInfos { get; set; }
        public List<Expert> Experts { get; set; }
        public List<Profession> Professions { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
