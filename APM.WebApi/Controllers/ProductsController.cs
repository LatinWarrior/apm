using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using APM.WebApi.Models;

namespace APM.WebApi.Controllers
{
    [EnableCorsAttribute("http://localhost:63904", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery]
        public IQueryable<Product> Get()
        {
            var productRepository = new ProductRepository();
            return productRepository.Retrieve().AsQueryable();
        }

        /// OData renders this method unnecessary.        
        //public IEnumerable<Product> Get(string search)
        //{
        //    var productRepository = new ProductRepository();
        //    var products = productRepository.Retrieve();
        //    return products.Where(x => x.ProductCode.Contains(search));
        //}

        // GET: api/Products/5
        public Product Get(int id)
        {
            Product product;
            var productRepository = new ProductRepository();

            if (id > 0)
            {
                var products = productRepository.Retrieve();
                product = products.FirstOrDefault(x => x.ProductId == id);
            }
            else
            {
                product = productRepository.Create();
            }

            return product;
        }

        // POST: api/Products
        public Product Post([FromBody]Product product)
        {
            var productRepository = new ProductRepository();
            var newProduct = productRepository.Save(product);
            return newProduct;
        }

        // PUT: api/Products/5
        public Product Put(int id, [FromBody]Product product)
        {
            var productRepository = new ProductRepository();
            var updatedProduct = productRepository.Save(id, product);
            return updatedProduct;
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
