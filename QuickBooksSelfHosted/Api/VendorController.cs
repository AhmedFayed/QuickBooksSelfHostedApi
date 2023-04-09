using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class VendorController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public VendorController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]string fullName)
        {
            return Ok(_quickBooksService.GetVendor(fullName));
        }
        
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_quickBooksService.GetVendors());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] VendorAdd vendorAdd)
        {
            var result = _quickBooksService.AddVendor(vendorAdd);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }
    }
}
