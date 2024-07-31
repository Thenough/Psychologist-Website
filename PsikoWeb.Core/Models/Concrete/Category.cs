using Core.Models.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Concrete
{
    public class Category : ICatogory
    {
        public int Id { get ; set; }
        public string Name { get ; set ; }
        public ICollection<Product> Products { get ; set ; }
        
    }
}
