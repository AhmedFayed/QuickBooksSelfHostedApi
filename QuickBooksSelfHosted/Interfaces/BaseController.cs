using QuickBooksSelfHostedApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickBooksSelfHostedApi.Interfaces
{
    class BaseController : ApiController
    {
        public readonly QuickBooksService _quickBooksService;
        public BaseController(QuickBooksService quickBooksService)
        {
            _quickBooksService = quickBooksService;
        }
    }
}
