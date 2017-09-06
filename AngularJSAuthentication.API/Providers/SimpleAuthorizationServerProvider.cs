﻿
using AngularJSAuthentication.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularJSAuthentication.API.Providers;
using System.Web;

namespace AngularJSAuthentication.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public Logger logger = LogManager.GetCurrentClassLogger();
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            using (AuthRepository _repo = new AuthRepository())
            {
                client = _repo.FindClient(context.ClientId);
            }

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            try
            {
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
                IdentityUser user = null;
                using (AuthRepository _repo = new AuthRepository())
                {
                    user = await _repo.FindUser(context.UserName, context.Password);
                    if (user == null)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }


                }
                iAuthContext con = new AuthContext();
                //
                People p = con.getPersonIdfromEmail(user.Email);
                int UserId = p.PeopleID;
                if (!p.Active)
                {
                    context.SetError("invalid_grant", "Please check your registered email address to validate email address.");
                    return;
                }
                if (UserId == 0)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, p.Permissions));
                //     identity.AddClaim(new Claim("displayname", p.DisplayName));
                identity.AddClaim(new Claim("firsttime", "true"));
                identity.AddClaim(new Claim("compid", user.PhoneNumber));
                identity.AddClaim(new Claim("email", user.Email));
                identity.AddClaim(new Claim("userid", UserId.ToString()));
                var props = new AuthenticationProperties(new Dictionary<string, string> { });                    
                if (p.Permissions == "Supplier")
                {
                    props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        { "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                        { "userName", context.UserName },

                        { "compid", user.PhoneNumber },
                        { "role" ,p.Permissions },
                        { "email" ,user.Email },
                        { "userid", UserId.ToString() },
                        { "Skcode",  p.Skcode }
                    });
                }
                else
                {
                    props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        { "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                        { "userName", context.UserName },

                        { "compid", user.PhoneNumber },
                        { "role" ,p.Permissions },
                        { "email" ,user.Email },
                        { "userid", UserId.ToString() }
                    });
                }
                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            catch (Exception ex)
            {
                logger.Error("Unable to validate user {0}", context.UserName);
                logger.Error(ex.Message);
            }
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;
            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }
            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}