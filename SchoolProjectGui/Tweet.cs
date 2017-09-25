using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectGui
{
    public class Tweet
    {
        public int TweetUpvotes { get; private set; }
        public int TweetDownvotes { get; private set; }
        public Int64 TweetID { get; }
        public string TweetText { get; private set; }
        public DateTime TweetTimeStamp { get; }

        public Tweet(Int64 ID, string text, DateTime timestamp, int Upvotes, int Downvotes)
        {
            TweetID = ID;
            TweetText = text;
            TweetTimeStamp = timestamp;
            TweetUpvotes = Upvotes;
            TweetDownvotes = Downvotes;
        }

        public void Updatetext(string newText)
        {
            TweetText = newText;
        }

        public override string ToString()
        {
            return "[" + TweetTimeStamp.ToString() + " - " + TweetID.ToString() + "] " + TweetText;
        }

        public static string Base64Encode(string plainText)
        {
            if (plainText != string.Empty)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            else
                return plainText;
        }

        public static string Base64Decode(string base64EncodedData)
        {
            if (base64EncodedData != string.Empty)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            else
                return base64EncodedData;
        }

        public void Upvote()
        {
            TweetUpvotes++;
        }

        public void Downvote()
        {
            TweetDownvotes++;
        }

    }
}
