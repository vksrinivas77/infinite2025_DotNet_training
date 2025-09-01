using System.Linq;
using System.Web.Http;
using dbfirst_cc;
using dbfirst_cc.Models; // Adjust namespace as appropriate

public class OrdersController : ApiController
{
    private northwindEntities db = new northwindEntities();

    // GET api/orders/employee/5
    [HttpGet]
    [Route("api/orders/employee/{employeeId}")]
    public IHttpActionResult GetOrdersByEmployeeId(int employeeId)
    {
        var orders = db.Orders
            .Where(o => o.EmployeeID == employeeId)
            .Select(o => new OrderDto
            {
                OrderID = o.OrderID,
                CustomerID = o.CustomerID,
                EmployeeID = o.EmployeeID,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                ShipVia = o.ShipVia,
                Freight = o.Freight,
                ShipName = o.ShipName,
                ShipAddress = o.ShipAddress,
                ShipCity = o.ShipCity,
                ShipRegion = o.ShipRegion,
                ShipPostalCode = o.ShipPostalCode,
                ShipCountry = o.ShipCountry
                
            })
            .ToList();

        return Ok(orders);
    }
}
