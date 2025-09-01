using System.Linq;
using System.Web.Http;

namespace CompanyOrderApi
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private northwindEntities db = new northwindEntities();

        // GET: api/customers/bycountry?country=Germany
        [HttpGet]
        [Route("bycountry")]
        public IHttpActionResult GetCustomersByCountry(string country)
        {
            var customers = db.GetCustomersByCountry(country).ToList();
            if (!customers.Any())
                return NotFound();
            return Ok(customers);
        }
    }
}
