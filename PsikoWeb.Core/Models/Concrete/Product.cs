using Core.Models.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Concrete
{
    public class Product : IProduct
    {
        public int Id {get ; set; }
        public string Name { get; set; }
        public decimal Price { get; set ; }
        public int Stock { get; set; }
    }
}
