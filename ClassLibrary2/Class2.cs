using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Class2
    {
        public  int id = 4;
        public string token = "QpwL5tke4Pnpja7X4";

        public SuccessfulRegistration SuccessfulResultRegistration(IRestResponse response)
        {
            var content = response.Content;
            var resultRegistration = JsonConvert.DeserializeObject<SuccessfulRegistration>(content);

            return resultRegistration;
        }

        public UnSuccessfulRegistration UnSuccessfulRegistration(IRestResponse response)
        {
            var content = response.Content;
            var resultRegistration = JsonConvert.DeserializeObject<UnSuccessfulRegistration>(content);

            return resultRegistration;
        }
    }
}
