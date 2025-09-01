using dbfirst_cc;
using System.Linq;
using System.Web.Http;

namespace dbfirst_cc.Controllers
{
    public class OrdersController : ApiController
    {
        private northwindEntities db = new northwindEntities();

        // GET api/orders/buchanan
        // GET api/orders/employee/5
        [HttpGet]
        [Route("api/orders/employee/{employeeId}")]
        public IHttpActionResult GetOrdersByEmployeeId(int employeeId)
        {
            var orders = db.Orders
                .Where(o => o.EmployeeID == employeeId)
                .ToList();
            return Ok(orders);
        }
    }
}
