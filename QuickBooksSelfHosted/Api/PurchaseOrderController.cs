using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class PurchaseOrderController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public PurchaseOrderController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] PurchaseOrderAdd purchaseOrder)
        {
            var result = _quickBooksService.AddPurchaseOrder(purchaseOrder);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] string id)
        {
            return Ok(_quickBooksService.GetPurchaseOrder(id));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_quickBooksService.GetPurchaseOrders());
        }
    }
}
