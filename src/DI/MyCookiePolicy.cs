using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RicardoGaefke.Data;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
  private readonly MyIdentity _myIdentity;

  public CustomCookieAuthenticationEvents(MyIdentity MyIdentity)
  {
    _myIdentity = MyIdentity;
  }

  public override Task SignedIn(CookieSignedInContext context)
  {

    return Task.CompletedTask;
  }

  public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
  {
    var userID = (from c in context.HttpContext.User.Claims
                  where c.Type == "UserID"
                  select c.Value).FirstOrDefault()
    ;
    
    string _url = context.Request.Host + context.Request.Path;

    if (!string.IsNullOrEmpty(userID))
    {
      _myIdentity.Record(Convert.ToInt32(userID), 10, _url);
    }

    context.Response.StatusCode = 403;

    string _host = context.Request.Host.ToString();
    string _identity = "https://identity.ricardogaefke.com/403";

    if (_host.Contains("localhost"))
    {
      _identity = "https://localhost:5055/403";
    }

    if (_host.Contains("staging"))
    {
      _identity = "https://identity.staging.ricardogaefke.com/403";
    }

    if (!_host.Contains("api") && !_host.Contains("localhost:5065"))
    {
      context.Response.Redirect(_identity);
    }

    return Task.CompletedTask;
  }

  public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
  {
    string _host = context.Request.Host.ToString();
    string _identity = "identity.ricardogaefke.com";

    if (_host.Contains("localhost"))
    {
      _identity = "localhost:5055";
    }

    if (_host.Contains("staging"))
    {
      _identity = "identity.staging.ricardogaefke.com";
    }

    string _url = context.Request.Host + context.Request.Path;

    context.HttpContext.Response.Redirect($"https://{_identity}?ReturnUrl=https://" + _url);

    return Task.CompletedTask;
  }

  public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
  {
    var userPrincipal = context.Principal;

    // Look for the LastChanged claim.
    var lastChanged = (from c in userPrincipal.Claims
                       where c.Type == "LastChanged"
                       select c.Value).FirstOrDefault()
    ;

    // Look for the userID claim.
    var userID = (from c in userPrincipal.Claims
                       where c.Type == "UserID"
                       select c.Value).FirstOrDefault()
    ;

    string _url = context.Request.Host + context.Request.Path;

    if (string.IsNullOrEmpty(lastChanged) || !_myIdentity.ValidateLastChanged(userID, lastChanged, _url).Success)
    {
      context.RejectPrincipal();

      await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
  }
}
