using CustomLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace SchoolProjectServer
{
    class TweetStyle
    {
        public string Name { get; }
        private OrderedDictionary properties = new OrderedDictionary();

        public TweetStyle(string name)
        {
            if (name != string.Empty)
                Name = name;
            else
                Name = "TweetStyle";
        }

        public void AddProperty(string original, string modified)
        {
            if (original != string.Empty && modified != string.Empty)
            {
                
                if (properties.Contains(original))
                    this.Log(LogExtension.LogLevels.Warning, original + " is already among properties", "Events");
                else
                    properties.Add(original, modified);
            }
        }

        public void ModifyProperty(string original = null, string modified = null)
        {

        }
    }
}
