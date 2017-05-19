using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectServer
{
    public class Tweet
    {
        public string TweetID { get; }
        public string TweetText { get; }
        public DateTime TweetTimeStamp { get; }

        public Tweet(string ID, string text, DateTime timestamp)
        {
            TweetID = ID;
            TweetText = text;
            TweetTimeStamp = timestamp;
        }

        public override string ToString()
        {
            return "[" + TweetTimeStamp.ToString() + " - " + TweetID + "] " + TweetText;
        }
    }
}
