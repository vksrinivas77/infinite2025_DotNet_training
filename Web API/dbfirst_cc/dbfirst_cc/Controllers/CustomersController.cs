using System.Web.Http;
using dbfirst_cc;
using System.Data.SqlClient;
using System.Linq;

namespace dbfirst_cc.Controllers
{
    public class CustomersController : ApiController
    {
        private northwindEntities db = new northwindEntities();

        // GET api/customers/bycountry?country=?
        [HttpGet]
        [Route("api/customers/bycountry")]
        public IHttpActionResult GetCustomersByCountry(string country)
        {
            var countryParam = new SqlParameter("@Country", country);
            var customers = db.Database
                .SqlQuery<Customer>("EXEC GetCustomersByCountry @Country", countryParam)
                .ToList();
            return Ok(customers);
        }
    }
}
