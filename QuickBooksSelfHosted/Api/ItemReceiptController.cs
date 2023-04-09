using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class ItemReceiptController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public ItemReceiptController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] ItemReceiptAdd itemReceiptAdd)
        {
            var result = _quickBooksService.AddItemReceipt(itemReceiptAdd);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]string id)
        {
            return Ok(_quickBooksService.GetItemReceipt(id));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_quickBooksService.GetItemReceipts());
        }
    }

}
