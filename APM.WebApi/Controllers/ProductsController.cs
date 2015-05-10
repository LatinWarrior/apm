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

        public IEnumerable<Product> Get(string search)
        {
            var productRepository = new ProductRepository();
            var products = productRepository.Retrieve();
            return products.Where(x => x.ProductCode.Contains(search));
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
