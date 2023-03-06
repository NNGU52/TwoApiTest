using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1 
    {

        public ListsOfUsers GetUsers(IRestResponse response)
        {
            var content = response.Content;
            Console.WriteLine(content);
            var users = JsonConvert.DeserializeObject<ListsOfUsers>(content);

            // вывод в консоль всех пользователей
            for (int i = 0; i < users.data.Count; i++)
            {
                Console.WriteLine(users.data[i].first_name + " " + users.data[i].last_name);

            }

            return users;


        }
    }
}
