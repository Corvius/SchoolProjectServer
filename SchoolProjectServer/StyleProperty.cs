using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectServer
{
    class StyleProperty
    {
        public string Original { get; set; }
        public string Modified { get; set; }

        public StyleProperty(string original, string modified)
        {
            Original = original;
            Modified = modified;
        }
    }
}
