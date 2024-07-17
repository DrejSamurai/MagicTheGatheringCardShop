using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedSystems.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly IOrderService _orderService;

        public ApiController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return this._orderService.GetAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetails(BaseEntity id)
        {
            return this._orderService.GetDetailsForOrder(id);
        }
    }
}
