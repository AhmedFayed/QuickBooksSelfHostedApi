using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class CustomerController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public CustomerController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]string fullName)
        {
            return Ok(_quickBooksService.GetCustomer(fullName));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_quickBooksService.GetCustomers());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] CustomerAdd customerAdd)
        {
            var result = _quickBooksService.AddCustomer(customerAdd);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }
    }
}
