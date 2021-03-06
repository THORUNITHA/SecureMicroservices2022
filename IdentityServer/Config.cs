using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
      new Client[]
      {
          new Client
          {
              ClientId ="studentClient",
              AllowedGrantTypes = GrantTypes.ClientCredentials,
              ClientSecrets =
              {
                  new Secret("secret".Sha256())
              },
              AllowedScopes ={"studentAPI"}
          },
             new Client
                {
                    ClientId="ro.client",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
          
                    AllowedScopes={ "studentAPI" }
                },
         new Client
          {
             
              ClientId="students_mvc_client",
              ClientName ="Students MVC Web App",
              AllowedGrantTypes=GrantTypes.Hybrid,
              RequirePkce=false,
              AllowRememberConsent=false,
              RedirectUris =new List<string>
              {
                  "https://localhost:5002/signin-oidc"
              },
              PostLogoutRedirectUris =new List<string>
              {
                  "https://localhost:5002/signout-callback-oidc"
              },
              ClientSecrets =new List<Secret>
              {
                  new Secret("secret".Sha256())
              },
              AllowedScopes=new List<string>
              {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  IdentityServerConstants.StandardScopes.Address,
                  IdentityServerConstants.StandardScopes.Email,
                  "studentAPI",
                  "roles"
              }
          }
            /*    new Client
                {
                 ClientId = "students_mvc_client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                    
                AllowedGrantTypes = GrantTypes.Code,

                 RedirectUris = { "https://localhost:5002/signin-oidc" },
                //    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                 PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                // AllowOfflineAccess = true,
                 AllowedScopes = new List<string>{
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile}
                }*/

      };

        public static IEnumerable<ApiScope> ApiScopes =>
       new ApiScope[]
       {
           new ApiScope("studentAPI", "student API")
       };

        public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
     new IdentityResource[]
     {
         new IdentityResources.OpenId(),
         new IdentityResources.Profile(),
          new IdentityResources.Address(),
          new IdentityResources.Email(),
          new IdentityResource(
              "roles",
              "Your role(s)",
              new List<string>(){"role"}) 
         

     };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser{
                SubjectId="5BE86359-073C-434B-AD2D-A3932222DABE",
                Username="thoru",
                Password="thoru",
                Claims=new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName,"thoru"),
                    new Claim(JwtClaimTypes.FamilyName,"ss")

                }
                },
            };


    }
}
 