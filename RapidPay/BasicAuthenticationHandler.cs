using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(
                AuthenticateResult.Fail("No authorization in header were found")
            );
        }
        var authHeader = Request.Headers["Authorization"].ToString();
        if(!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(
                AuthenticateResult.Fail("Invalid authenticate scheme")
            );
        }

        var authBase64Decoded = Encoding.UTF8.GetString(
            Convert.FromBase64String(authHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)
            )
        );

        var authSplit = authBase64Decoded.Split(new char[] { ':' });
        var username = authSplit[0];
        var password = authSplit[1];

        if(username == "admin" && password == "Test1")
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        return Task.FromResult(AuthenticateResult.Fail("Invalid usernaem or pasword"));

    }
}