using QuickBooksSelfHostedApi.Services;
using System.Web.Http;

namespace ConsoleApp1.Api
{
    public class ValuesController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public ValuesController()
        {
            _quickBooksService = new QuickBooksService();
        }
        // GET api/values 
        public IHttpActionResult Get()
        {
            return Ok(_quickBooksService.GetInventoryItems());
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
