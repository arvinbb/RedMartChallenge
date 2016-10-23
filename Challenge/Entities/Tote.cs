using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Entities
{

    public class Tote
    {
        private List<Product> _products;

        public List<Product> Products => _products != null ? _products : _products = new List<Product>();
                
        public int ProductIdSum => Products.Sum(product => product.Id); 

        public int TotalWeight => Products.Sum(product => product.Weight); 

        public int TotalVolume => Products.Sum(product => product.Volume);

        public int TotalPrice => Products.Sum(product => product.Price);
        
        public int TotalMergedPriceAndWeight => Products.Sum(product => product.MergedPriceAndWeight);
         
    }
}
