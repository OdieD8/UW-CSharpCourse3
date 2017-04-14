using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Models;

namespace HelloWorld.Tests
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products
        {
            get
            {
                var items = new[]
                {
                    new Product{ Name = "Baseball", Price = 11m },
                    new Product{ Name = "Football", Price = 8m },
                    new Product{ Name = "Tennis ball", Price = 13m },
                    new Product{ Name = "Golf ball", Price = 3m },
                    new Product{ Name = "Ping Pong ball", Price = 12m }
                };
                return items;
            }
        }
    }
}
