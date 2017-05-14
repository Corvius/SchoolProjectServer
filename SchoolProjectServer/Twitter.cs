using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SchoolProjectServer
{
    public class Twitter
    {
        public string OAuthConsumerKey { get; private set; }
        public string OAuthConsumerSecret { get; private set; }

        private const string defaultOAuthCKey = "dEbSANjb9J0ZmWMCYbTcP2tFf";
        private const string defaultOAuthCSecret = "lSSrZfEx8t2YfMmZSOjVRsc7jpepasVslyEHHYRj7oqVqrsPju";

        public Twitter(string pOAuthConsumerKey = defaultOAuthCKey, string pOAuthConsumerSecret = defaultOAuthCSecret)
        {
            OAuthConsumerKey = pOAuthConsumerKey;
            OAuthConsumerSecret = pOAuthConsumerSecret;
        }

        public async Task<IEnumerable<string>> GetTweets(string userName, int tweetCount, string accessToken = null)
        {
            if (accessToken == null)
                accessToken = await GetAccessToken();

            string request = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", tweetCount, userName);
            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, request);
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            var serializer = new JavaScriptSerializer();
            dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            var enumerableTweets = (json as IEnumerable<dynamic>);

            if (enumerableTweets == null)
                return null;

            //var twi = enumerableTweets.ToList();
            //foreach (var tw in twi)
            //    foreach (var t in tw)
            //        Console.WriteLine("#### " + t.ToString());

            //return enumerableTweets.Select(tweets => (string)(tweets["text"].ToString()));
            return enumerableTweets.Select(tweets => (string)(tweets["created_at"].ToString()));
        }

        public async Task<string> GetAccessToken()
        {           
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            return  item["access_token"];            
        }
    }
}