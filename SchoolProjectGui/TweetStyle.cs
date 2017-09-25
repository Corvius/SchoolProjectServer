using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SchoolProjectGui
{
    public class TweetStyle
    {
        public string styleName { get; }
        public string styleImageURL { get; }
        public List<StyleProperty> styleProperties { get; }

        public TweetStyle(string pStyleName, string pStyleImageURL)
        {
            styleName = pStyleName;
            styleImageURL = pStyleImageURL;
            styleProperties = new List<StyleProperty>();
        }

        public void AddProperty(string original, string replacement)
        {
            styleProperties.Add(new StyleProperty(original, replacement));
        }

        public void DeleteProperty(string original)
        {
            foreach (var styleProperty in styleProperties)
                if (styleProperty.Original == original)
                {
                    styleProperties.Remove(styleProperty);
                    break;
                }
        }

        public byte[] ToByteArray()
        {
            MemoryStream lMs = new MemoryStream();
            byte[] lStyleNameArray = Encoding.ASCII.GetBytes(styleName);
            byte[] lStyleImageURLArray = Encoding.ASCII.GetBytes(styleImageURL);
            lMs.Write(lStyleNameArray, 0, lStyleNameArray.Length);
            lMs.Write(lStyleImageURLArray, 0, lStyleImageURLArray.Length);
            return lMs.ToArray();
        }
    }
}
