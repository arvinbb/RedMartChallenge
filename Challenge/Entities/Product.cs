using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Entities
{   
    public class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Volume => Length * Width * Height;
        /// <summary>
        /// Merged price and weight
        /// Price is multiplied to arbitrary value of 10000 so that it will not be affected by Weight that much.
        /// 3600 is arbitrary value to reverse the value of Weight, since 3540 is the max product weight.
        /// </summary>
        public int MergedPriceAndWeight => Price * 10000 + (3600 - Weight);
    }
}
