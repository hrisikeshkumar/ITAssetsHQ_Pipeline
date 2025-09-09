using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace IT_Hardware.Controllers
{
    public class Log_InController : Controller
    {
        // GET: Log_In
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Log_In()
        {
            return View("Log_In");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Sign_In()
        {
            return RedirectToAction("Admin_Dashboard", "Admin_Dashboard", new { area = "Admin" });
        }

        public ActionResult CustomAccessDenied()
        {
            return View();
        }


        //--------------------------------------------------------------------------------------

        private readonly IOptionsMonitor<MicrosoftIdentityOptions> _optionsMonitor;

        /// <summary>
        /// Constructor of <see cref="AccountController"/> from <see cref="MicrosoftIdentityOptions"/>
        /// This constructor is used by dependency injection.
        /// </summary>
        /// <param name="microsoftIdentityOptionsMonitor">Configuration options.</param>
        
        public Log_InController(IOptionsMonitor<MicrosoftIdentityOptions> microsoftIdentityOptionsMonitor)
        {
            _optionsMonitor = microsoftIdentityOptionsMonitor;
        }

        /*

        /// <summary>
        /// Handles user sign in.
        /// </summary>
        /// <param name="scheme">Authentication scheme.</param>
        /// <param name="redirectUri">Redirect URI.</param>
        /// <returns>Challenge generating a redirect to Azure AD to sign in the user.</returns>
        //[HttpGet("{scheme?}")]
        //public IActionResult Log_In(
        //    [FromRoute] string scheme,
        //    [FromQuery] string redirectUri)
        //{
        //    scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
        //    string redirect;
        //    if (!string.IsNullOrEmpty(redirectUri) && Url.IsLocalUrl(redirectUri))
        //    {
        //        redirect = redirectUri;
        //    }
        //    else
        //    {
        //        redirect = Url.Content("~/Log_In/Log_In1")!;
        //    }

        //    return Challenge(
        //        new AuthenticationProperties { RedirectUri = redirect },
        //        scheme);
        //}
        */
        /// <summary>
        /// Challenges the user.
        /// </summary>
        /// <param name="redirectUri">Redirect URI.</param>
        /// <param name="scope">Scopes to request.</param>
        /// <param name="loginHint">Login hint.</param>
        /// <param name="domainHint">Domain hint.</param>
        /// <param name="claims">Claims.</param>
        /// <param name="policy">AAD B2C policy.</param>
        /// <param name="scheme">Authentication scheme.</param>
        /// <returns>Challenge generating a redirect to Azure AD to sign in the user.</returns>
        //[HttpGet("{scheme?}")]
        public IActionResult Challenge(
            string redirectUri,
            string scope,
            string loginHint,
            string domainHint,
            string claims,
            string policy,
            [FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            Dictionary<string, string?> items = new Dictionary<string, string?>
            {
                { Constants.Claims, claims },
                { Constants.Policy, policy },
            };
            Dictionary<string, object?> parameters = new Dictionary<string, object?>
            {
                { Constants.LoginHint, loginHint },
                { Constants.DomainHint, domainHint },
            };

            OAuthChallengeProperties oAuthChallengeProperties = new OAuthChallengeProperties(items, parameters);
            if (scope != null)
            {
                oAuthChallengeProperties.Scope = scope.Split(" ");
            }
            oAuthChallengeProperties.RedirectUri = redirectUri;

            return Challenge(
                oAuthChallengeProperties,
                scheme);
        }

        

        /// <summary>
        /// Handles the user sign-out.
        /// </summary>
        /// <param name="scheme">Authentication scheme.</param>
        /// <returns>Sign out result.</returns>
        public IActionResult Log_Out(
            [FromRoute] string scheme)
        {
            if (AppServicesAuthenticationInformation.IsAppServicesAadAuthenticationEnabled)
            {
                if (AppServicesAuthenticationInformation.LogoutUrl != null)
                {
                    return LocalRedirect(AppServicesAuthenticationInformation.LogoutUrl);
                }
                return Ok();
            }
            else
            {
                scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
                var callbackUrl = Url.Page("/Log_In/Log_In", pageHandler: null, values: null, protocol: Request.Scheme);
                return SignOut(
                     new AuthenticationProperties
                     {
                         RedirectUri = callbackUrl,
                     },
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     scheme);
            }
        }



        /// <summary>
        /// In B2C applications handles the Reset password policy.
        /// </summary>
        /// <param name="scheme">Authentication scheme.</param>
        /// <returns>Challenge generating a redirect to Azure AD B2C.</returns>
        //[HttpGet("{scheme?}")]
        public IActionResult ResetPassword([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;

            var redirectUrl = Url.Content("~/");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[Constants.Policy] = _optionsMonitor.Get(scheme).ResetPasswordPolicyId;
            return Challenge(properties, scheme);
        }



        /// <summary>
        /// In B2C applications, handles the Edit Profile policy.
        /// </summary>
        /// <param name="scheme">Authentication scheme.</param>
        /// <returns>Challenge generating a redirect to Azure AD B2C.</returns>
        //[HttpGet("{scheme?}")]
        public async Task<IActionResult> EditProfile([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            var authenticated = await HttpContext.AuthenticateAsync(scheme).ConfigureAwait(false);
            if (!authenticated.Succeeded)
            {
                return Challenge(scheme);
            }

            var redirectUrl = Url.Content("~/");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[Constants.Policy] = _optionsMonitor.Get(scheme).EditProfilePolicyId;
            return Challenge(properties, scheme);
        }


    }
} 


