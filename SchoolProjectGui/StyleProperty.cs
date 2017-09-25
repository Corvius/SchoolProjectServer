using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectGui
{
    public class StyleProperty
    {
        public string Original { get; set; }
        public string Replacement { get; set; }

        public StyleProperty(string original, string replacement)
        {
            Original = original;
            Replacement = replacement;
        }
    }
}
