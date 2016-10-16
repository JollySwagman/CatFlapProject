using CatFlap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace CatFlap.Controllers
{
    public class ProductsController : ApiController
    {
        private Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        public string logFile = @"C:\Users\d\Documents\Visual Studio 2015\Projects\CatFlap\CatFlap\App_Data\test.txt";

        [HttpPost]
        public IHttpActionResult Post([FromBody]string value)
        {
            //var x = value.var1.Value; // JToken

            var result = "value is null";
            if (value != null)
            {
                result = "!!!" + value;
            }

            // Creates a record...
            File.AppendAllText(logFile, DateTime.Now.ToString() + "--> " + result + Environment.NewLine);
            //File.AppendAllText("hits.txt", DateTime.Now.ToString() + "--> " + Environment.NewLine);
            return Ok();
        }

        //[HttpGet]
        public IHttpActionResult Records(int title)
        {
            // Creates a record...
            File.AppendAllText(logFile, DateTime.Now.ToString() + "--> " + title + Environment.NewLine);
            return Ok();
        }
    }
}