using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class InvoiceController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public InvoiceController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] InvoiceAdd invoiceAdd)
        {
            var result = _quickBooksService.AddInvoice(invoiceAdd);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] InvoiceMod invoiceMod)
        {
            var result = _quickBooksService.ModifyInvoice(invoiceMod);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] string id)
        {
            return Ok(_quickBooksService.GetInvoice(id));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_quickBooksService.GetInvoices());
        }

        [HttpPost]
        [Route("api/invoice/GetMany")]
        public IHttpActionResult GetMany(IReadOnlyCollection<string> identefirs)
        {
            return Ok(_quickBooksService.GetInvoices(identefirs));
        }
    }
}
