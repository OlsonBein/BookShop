using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogicLayer.Models.Cart;
using Store.BusinessLogicLayer.Models.Orders;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.Presentation.Helpers.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Jwt.Constants;
using static Store.DataAccessLayer.Common.Constants.Constants;

namespace Store.Presentation.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IJwtHelper _jwtHelper;
        public OrderController(IOrderService orderService, IJwtHelper jwtHelper)
        {
            _orderService = orderService;
            _jwtHelper = jwtHelper;
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllAsync(OrdersFilterModel model)
        {
            var orders = await _orderService.GetAllAsync(model);
            return Ok(orders);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CartModelItem model)
        {
           
            var token = HttpContext.Request.Headers
                .Where(x => x.Key == JwtConstants.RefreshToken)
                .Select(x => x.Value).FirstOrDefault();
            var userId = _jwtHelper.GetIdFromToken(token);
            var result = await _orderService.MakeOrderAsync(model, userId);                                                           
            return Ok(result);
        }  
        
        [HttpPost("getByUserId")]
        public async Task<IActionResult> GetByUserIdAsync(OrdersFilterModel model, long id)
        {
            var orders = await _orderService.GetUserOrdersAsync(model, id);
            return Ok(orders);
        }
    }
}