using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary5
{
    public partial class UpdateDate
    {
        public string name { get; set; }
        public string job { get; set; }

        public UpdateDate(string name_, string job_)
        {
            name = name_;
            job = job_;
        }
                
    }
}
