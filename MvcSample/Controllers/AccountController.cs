using System;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using Microsoft.IdentityModel.Web;

namespace Mvc3Sample.Controllers
{
    public class AccountController : Controller
    {

        private string domain = ConfigurationManager.AppSettings["auth0:Domain"];
        private string clientId = ConfigurationManager.AppSettings["auth0:ClientId"];
        private string audience = ConfigurationManager.AppSettings["auth0:Audience"];
        public ActionResult Login(string returnUrl)
        {
            string callbackUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/LoginCallback.ashx";
            string userInfoUrl = string.Format("https://{0}/userinfo", ConfigurationManager.AppSettings["auth0:Domain"]);
            string audience = this.audience ?? userInfoUrl;

            Random r = new Random();
            int rInt = r.Next(0, 100); //for ints
            int range = 100;
            double nonce = r.NextDouble() * range; //for doubles

            var state = "ru=" + System.Web.HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/'));

        

            return new RedirectResult(string.Format("https://{0}/authorize?client_id={1}&scope=openid profile offline_access&redirect_uri={2}&response_type=code&audience=urn:test:api&state={4}&nonce={5}",
        ConfigurationManager.AppSettings["auth0:Domain"], ConfigurationManager.AppSettings["auth0:ClientId"], callbackUrl, audience, state, nonce.ToString()));
        }

        public ActionResult Logout()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            // Redirect to Auth0's logout endpoint.
            // After terminating the user's session, Auth0 will redirect to the 
            // returnTo URL, which you will have to add to the list of allowed logout URLs for the client.
            var returnTo = Url.Action("Index", "Home", null, protocol: Request.Url.Scheme);
            return Redirect(
              string.Format(CultureInfo.InvariantCulture,
                "https://{0}/v2/logout?returnTo={1}&client_id={2}",
                ConfigurationManager.AppSettings["auth0:Domain"],
                Server.UrlEncode(returnTo),
                ConfigurationManager.AppSettings["auth0:ClientId"]));
        }
    }
}