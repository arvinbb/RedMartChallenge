using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Challenge.Entities;

namespace Challenge
{
    /// <summary>
    /// Main class for the challenge
    /// </summary>
    public class RM1MillionChallenge
    {
        const int _totalToteVolume = 45 * 30 * 35;
        
        public Tote Process(string productsFilePath)
        {  
            // Read contents of products file
            string[] productsFromFile = File.ReadAllLines(productsFilePath);

            // transform the CSV format to object
            var allProductList = productsFromFile.Select(product =>
            {
                var splitProduct = product.Split(',');
                return new Product
                {
                    Id = int.Parse(splitProduct[0]),
                    Price = int.Parse(splitProduct[1]),
                    Length = int.Parse(splitProduct[2]),
                    Width = int.Parse(splitProduct[3]),
                    Height = int.Parse(splitProduct[4]),
                    Weight = int.Parse(splitProduct[5])
                };
            })
            //Remove the products that doesn't fit individually
            .Where(product => product.Volume <= _totalToteVolume)
            .ToList();
            
            return GetOptimalTote(allProductList);
        }

        /// <summary>
        /// Computes the Tote with the highest price and lowest weight products in the Tote
        /// </summary>
        /// <param name="products">the list of products to process</param>
        /// <returns>returns the optimal tote</returns>
        private Tote GetOptimalTote(List<Product> products)
        {
            // this 2 dimensional array contains the list of all possible Tote volume per product
            Dictionary<int, List<Tote>> priceMatrix = new Dictionary<int, List<Tote>>(products.Count + 1);
            
            // iterate per product
            for (int productIndex = 0; productIndex <= products.Count; productIndex++)
            {
                // generate empty Totes from zero to the number of maximum volume
                priceMatrix[productIndex] = new List<Tote>(Enumerable.Range(0, _totalToteVolume + 1).Select(x => new Tote()).ToList());
                // save memory by removing the completed computations
                if (productIndex > 2)
                    priceMatrix[productIndex - 2] = null;
                // iterate per volume
                for (int volumeIndex = 0; volumeIndex <= _totalToteVolume; volumeIndex++)
                {
                    if (productIndex == 0 || volumeIndex == 0)
                        continue;
                    else if (products[productIndex - 1].Volume <= volumeIndex)
                    {
                        // check if MergedPriceAndWeight of (productIndex - 1) product plus the (productIndex - 1) of all products in the Tote is better
                        if (products[productIndex - 1].MergedPriceAndWeight + priceMatrix[productIndex - 1][volumeIndex - products[productIndex - 1].Volume].TotalMergedPriceAndWeight >= priceMatrix[productIndex - 1][volumeIndex].TotalMergedPriceAndWeight)
                        {
                            // add product to current Tote
                            priceMatrix[productIndex][volumeIndex].Products.Add(products[productIndex - 1]);
                            priceMatrix[productIndex][volumeIndex].Products.AddRange(priceMatrix[productIndex - 1][volumeIndex - products[productIndex - 1].Volume].Products);
                        }
                        else
                            priceMatrix[productIndex][volumeIndex] = priceMatrix[productIndex - 1][volumeIndex];
                    }
                    else
                        priceMatrix[productIndex][volumeIndex] = priceMatrix[productIndex - 1][volumeIndex];
                }
            }

            // last Tote computed is the optimal Tote
            return priceMatrix[products.Count][_totalToteVolume];
            
        }
        
    }
}
