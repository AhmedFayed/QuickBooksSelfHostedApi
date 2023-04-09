using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class BillController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public BillController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] BillAdd billAdd)
        {
            var result = _quickBooksService.AddBill(billAdd);
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
            return Ok(_quickBooksService.GetBills());
        }

        
        [Route("api/bill/GetMany")]
        [HttpPost]
        public IHttpActionResult GetMany(IReadOnlyCollection<string> identefirs)
        {
            return Ok(_quickBooksService.GetBills(identefirs));
        }
    }

}
