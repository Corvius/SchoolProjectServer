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
        public string PictureURL { get; }
        public List<StyleProperty> properties { get; }

        public TweetStyle(string name, string pictureURL)
        {
            properties = new List<StyleProperty>();

            Name = (name == string.Empty) ? "TweetStyle" : name;
            PictureURL = (pictureURL == null) ? "" : pictureURL;
        }

        public TweetStyle(string name) : this(name, "") { }

        public void AddProperty(string original, string replacement)
        {
            bool match = false;

            if (original != string.Empty && replacement != string.Empty)
            {
                foreach (StyleProperty property in properties)
                    if (property.Original == original)
                    {  
                        match = true;
                        break;
                    }

                if (match)
                    this.Log(LogExtension.LogLevels.Warning, original + " is already among properties", "Events");
                else
                    properties.Add(new StyleProperty(original, replacement));
            }
        }

        public void ModifyProperty(string original = null, string replacement = null)
        {
            foreach (StyleProperty property in properties)
                if (property.Original == original)
                    if (replacement != null)
                    {
                        this.Log(LogExtension.LogLevels.Info, "Property " + original + " has changed from " + property.Replacement + " to " + replacement, "Events");

                        property.Replacement = replacement;
                    }
        }
    }
}
