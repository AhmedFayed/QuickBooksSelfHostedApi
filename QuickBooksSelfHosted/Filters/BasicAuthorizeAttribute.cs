using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QuickBooksSelfHostedApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class BasicAuthorizeAttribute : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var identity = GetAuthHeader(filterContext);
            if (identity == null || !CheckIdentity(identity.UserName, identity.Password))
            {
                RequestCredentials(filterContext);
            }
        }

        private BasicAuthenticationIdentity GetAuthHeader(HttpActionContext filterContext)
        {
            var authRequest = filterContext.Request.Headers.Authorization;
            var authHeaderValue = authRequest?.Scheme == "Basic" ? authRequest.Parameter : null;

            if (string.IsNullOrEmpty(authHeaderValue))
            {
                return null;
            }
            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));
            var credentials = authHeaderValue.Split(':');
            return credentials.Length < 2 ? null : new BasicAuthenticationIdentity(credentials[0], credentials[1]);

        }

        private static void RequestCredentials(HttpActionContext filterContext)
        {
            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            filterContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", "Steel Network"/*dnsHost*/));
        }

        private bool CheckIdentity(string userName, string password)
        {
            //return userName == "admin" && password == "admin";
            return userName == "Admin4Fk3$Js28" && password == "passHjL33NEH@M5r";
        }
    }


    public class BasicAuthenticationIdentity
    {
        public string UserName { get; private set; }

        public string Password { get; private set; }

        public BasicAuthenticationIdentity(string userName, string password)
        {
            UserName = userName;
            Password = password;

        }
    }
}