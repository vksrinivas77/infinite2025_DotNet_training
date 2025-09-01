using System.Linq;
using System.Web.Http;

namespace CompanyOrderApi
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private northwindEntities db = new northwindEntities();

        // GET: api/orders/byemployee/5
        [HttpGet]
        [Route("byemployee/{employeeId}")]
        public IHttpActionResult GetOrdersByEmployee(int employeeId)
        {
            var orders = db.Orders.Where(o => o.EmployeeID == employeeId).ToList();
            if (!orders.Any())
                return NotFound();
            return Ok(orders);
        }
    }
}
