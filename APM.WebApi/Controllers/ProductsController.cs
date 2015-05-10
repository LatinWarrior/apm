using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.OData;
using APM.WebApi.Models;

namespace APM.WebApi.Controllers
{
    [EnableCorsAttribute("http://localhost:63904", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery]
        [ResponseType(typeof (Product))]
        public IHttpActionResult Get()
        {
            try
            {
                var productRepository = new ProductRepository();
                return Ok(productRepository.Retrieve().AsQueryable());
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        /// OData renders this method unnecessary.        
        //public IEnumerable<Product> Get(string search)
        //{
        //    var productRepository = new ProductRepository();
        //    var products = productRepository.Retrieve();
        //    return products.Where(x => x.ProductCode.Contains(search));
        //}

        // GET: api/Products/5
        [ResponseType(typeof (Product))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Product product;
                var productRepository = new ProductRepository();

                if (id > 0)
                {
                    var products = productRepository.Retrieve();
                    product = products.FirstOrDefault(x => x.ProductId == id);

                    if (product == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    product = productRepository.Create();
                }

                return Ok(product);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        // POST: api/Products
        [ResponseType(typeof (Product))]
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productRepository = new ProductRepository();
                var newProduct = productRepository.Save(product);

                if (newProduct == null)
                {
                    return Conflict();
                }

                return Created(Request.RequestUri + newProduct.ProductId.ToString(CultureInfo.InvariantCulture),
                    newProduct);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        // PUT: api/Products/5
        [ResponseType(typeof (Product))]
        public IHttpActionResult Put(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productRepository = new ProductRepository();
                var updatedProduct = productRepository.Save(id, product);

                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
