using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class ReceivePaymentController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public ReceivePaymentController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] ReceivePaymentAdd receivePaymentAdd)
        {
            var result = _quickBooksService.AddReceivePayment(receivePaymentAdd);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] string id)
        {
            return Ok(_quickBooksService.GetBill(id));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_quickBooksService.GetReceivePayment());
        }
    }

}
