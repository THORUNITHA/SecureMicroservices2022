using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Students.Client.ApiServices;
using Newtonsoft.Json;
using Students.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Students.Client.ApiServices
{
    public class StudentApiService : IStudentApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudentApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
           _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the access token");
            }

            var accessToken = await _httpContextAccessor
                .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
               new UserInfoRequest
               {
                   Address = metaDataResponse.UserInfoEndpoint,
                   Token = accessToken
               });

            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while getting user info");
            }

            var userInfoDictionary = new Dictionary<string, string>();

            foreach (var claim in userInfoResponse.Claims)
            {
                userInfoDictionary.Add(claim.Type, claim.Value);
            }

            return new UserInfoViewModel(userInfoDictionary);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var httpClient = _httpClientFactory.CreateClient("StudentAPIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/students");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            Console.WriteLine(response);
            Console.WriteLine(response.Headers);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var studentList = JsonConvert.DeserializeObject<List<Student>>(content);
            return studentList;

            /* Get Token from Identity Server ......*/
            //// 1. "retrieve" our api credentials. This must be registered on Identity Server!
            /*   var apiClientCredentials = new ClientCredentialsTokenRequest
               {
                   Address = "https://localhost:5005/connect/token",

                  ClientId = "studentClient",
                  ClientSecret = "secret",

               //    // This is the scope our Protected API requires. 
                Scope = "studentAPI"
              }; 
           //// creates a new HttpClient to talk to our IdentityServer (localhost:5005)
               var client = new HttpClient();

               //// just checks if we can reach the Discovery document. Not 100% needed but..
               var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
               if (disco.IsError)
              {
               return null; // throw 500 error
               }

               //// 2. Authenticates and get an access token from Identity Server
              var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);            
               if (tokenResponse.IsError)
               {
                 return null;
               }


       /* SEND REQUEST TO PROTECTED API */
            //// Another HttpClient for talking now with our Protected API
            /*    var apiClient = new HttpClient();

                 //// 3. Set the access_token in the request Authorization: Bearer <token>
                 apiClient.SetBearerToken(tokenResponse.AccessToken);

                 //// 4. Send a request to our Protected API
                 var response = await apiClient.GetAsync("https://localhost:5001/api/students");
                 response.EnsureSuccessStatusCode();

                 var content = await response.Content.ReadAsStringAsync();

                 var studentList = JsonConvert.DeserializeObject<List<Student>>(content);
                 return studentList;*/
            /*  var studentList = new List<Student>();
                studentList.Add(
                    
                        new Student
            {
                Id = 1,
                Name = "Thorunitha",
                Department = "MTech",
                Address = "Vellore",
                courses=2,
                Cgpa=8.78,
                ImageUrl = "images/src",        
                Owner = "alice"
            },
            new Student
            {
                Id=2,
                Name="Aswini",
                Department="CSE",
                Address="Chennai",
                courses=4,
                 Cgpa=7.3,
                ImageUrl="images/src",
                Owner="alice"
            },
             new Student
            {
                Id=3,
                Name="Janani",
                Department="ECE",
                Address="Canada",
                courses=6,
                 Cgpa=9.0,
                ImageUrl="images/src",
                Owner="bob"
            }
                    );
               return await Task.FromResult(studentList);*/
        }
        public Task<Student> CreateStudent(Student student)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteStudent(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Student> GetStudent(string id)
        {
            throw new System.NotImplementedException();
        }
        
 

        public Task<Student> UpdateStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
