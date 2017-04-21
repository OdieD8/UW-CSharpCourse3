using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelloWorld.Models;
using System.Collections.Generic;
using System.Web.Caching;

namespace HelloWorld
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }

    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products
        {
            get
            {
                if (HttpContext.Current.Cache["MyProducts"] == null)
                {
                    var items = new[]
                    {
                        new Product{ ProductId=101, Name = "Baseball", Description="balls", Price=14.20m},
                        new Product{ ProductId=102, Name="Football", Description="nfl", Price=9.24m},
                        new Product{ Name="Tennis ball"} ,
                        new Product{ Name="Golf ball"},
                    };

                    HttpContext.Current.Cache.Insert("MyProducts", items, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 5));
                }

                return (IEnumerable<Product>)HttpContext.Current.Cache["MyProducts"];
            }
        }
    }
}