using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public partial class Registration
    {
        public string email { get; set; }
        public string password { get; set; }

        public Registration(string email_, string password_)
        {
            email = email_;
            password = password_;
        }
    }
}
