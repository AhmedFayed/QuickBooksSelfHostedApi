using QuickBooksSelfHostedApi.Filters;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuickBooksAPI.Controllers
{
    [BasicAuthorize]
    public class ItemController : ApiController
    {
        private readonly QuickBooksService _quickBooksService;
        public ItemController()
        {
            _quickBooksService = new QuickBooksService();
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]string fullName)
        {
            return Ok(_quickBooksService.GetInventoryItem(fullName));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_quickBooksService.GetInventoryItems());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] ItemInventoryAdd itemInventoryAdd)
        {
            var result = _quickBooksService.AddInventoryItem(itemInventoryAdd);
            if (result.Item1 is null)
            {
                return BadRequest(result.Item2?.StatusMessage);
            }
            return Ok(result.Item1);
        }

        [HttpPatch]
        public IHttpActionResult Patch([FromBody] List<ItemInventoryAdd> itemsInventoryAdd)
        {
            var result = _quickBooksService.AddInventoryItems(itemsInventoryAdd);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
