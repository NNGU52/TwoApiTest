using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3
{
    public class Class3
    {

        public List<int> Years(IRestResponse response)
        {
            var content = response.Content;
            var users = JsonConvert.DeserializeObject<ListResource>(content);
            List<int> years = new List<int>();

            for (int i = 0; i < users.data.Count; i++)
            {
                years.Add(users.data[i].year);
            }

            return years;
        }
    }
}
