using ClassLibrary1;
using ClassLibrary2;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoApiTest
{
    public class Specification
    {
        private static string HostPrefix = "https://reqres.in/";

        // метод Get
        protected IRestResponse GetMethod(string urlRequest)
        {
            RestClient client = new RestClient(HostPrefix);

            RestRequest request = new RestRequest(urlRequest, Method.GET);
            IRestResponse response = client.Get(request);
            return response;
        }

        // метод Post
        protected IRestResponse PostMethod(string urlRequest, string payload)
        {
            RestClient client = new RestClient(HostPrefix);

            RestRequest request = new RestRequest(urlRequest, Method.POST);
            request.AddParameter("application/json", payload, ParameterType.RequestBody);
            IRestResponse response = client.Post(request);

            return response;
        }

        // метод Put
        protected IRestResponse PutMethod(string urlRequest, string payload)
        {
            RestClient client = new RestClient(HostPrefix);
            RestRequest request = new RestRequest(urlRequest, Method.PUT);
            request.AddParameter("application/json", payload, ParameterType.RequestBody);
            IRestResponse response = client.Put(request);

            return response;
        }

        // метод Delete
        protected IRestResponse DeleteMethod(string urlRequest)
        {
            RestClient client = new RestClient(HostPrefix);

            RestRequest request = new RestRequest("/api/users/2", Method.DELETE);
            IRestResponse response = client.Delete(request);

            return response;
        }

        protected int GetStatusCode(IRestResponse response)
        {
            int statusCode = (int)response.StatusCode;
            return statusCode;
        }

        protected int StatusCode200()
        {
            return 200;
        }

        protected int StatusCode400()
        {
            return 400;
        }

        protected int StatusCode204()
        {
            return 204;
        }
    }
}
