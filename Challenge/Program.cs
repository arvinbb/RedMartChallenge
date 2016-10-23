using System;
using Challenge.Entities;

namespace Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var rm1million = new RM1MillionChallenge();
            Tote result = rm1million.Process("products.csv");
            Console.WriteLine($"ProductId sum is {result.ProductIdSum}");
            Console.ReadLine();
        }
    }
    
}
