using Cooper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Cooper.Services
{
    class SocialAuth : ISocialAuth
    {
        readonly string FacebookAppID, FacebookSecretKey;
        readonly string getFacebookSecretKey;
        delegate bool authorizerDelegate(string token, string id);
        Dictionary<string, authorizerDelegate> CheckAuth = new Dictionary<string, authorizerDelegate>();

        public SocialAuth(IConfigProvider configProvider)
        {
            this.FacebookAppID = configProvider.FacebookProvider.AppID;
            this.FacebookSecretKey = configProvider.FacebookProvider.AppSecretKey;

            getFacebookSecretKey = $"https://graph.facebook.com/oauth/access_token?client_id={FacebookAppID}&client_secret={FacebookSecretKey}&grant_type=client_credentials";

            CheckAuth.Add("facebook", IsFacebookAuth);
            CheckAuth.Add("google", IsGoogleAuth);
        }

        public bool getCheckAuth(string provider, string token, string id)
        {
            return CheckAuth[provider](token, id);
        }

        private bool IsFacebookAuth(string token, string user_id)
        {
            bool result = false;
            string secretKey = GetFacebookSecretKey();
            string url = $"https://graph.facebook.com/debug_token?input_token={token}&access_token={secretKey}";
            try
            {
                var json_result = JObject.Parse(ParseURL(url));
                result = ((bool)(json_result.SelectToken("data").SelectToken("is_valid"))
                         && (string)(json_result.SelectToken("data").SelectToken("user_id")) == user_id);
            }
            catch { }

            return result;
        }

        private bool IsGoogleAuth(string idToken, string email)
        {
            bool result = false;
            string url = $"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}";
            try
            {
                var json_result = JObject.Parse(ParseURL(url));
                result = (string)(json_result.SelectToken("error")) == null
                    && (bool)(json_result.SelectToken("email_verified"))
                    && (string)(json_result.SelectToken("email")) == email;
            }
            catch { }

            return result;
        }

        private string GetFacebookSecretKey()
        {
            return (string)(JObject.Parse(ParseURL(getFacebookSecretKey)).SelectToken("access_token"));
        }

        private string ParseURL(string url)
        {
            string result = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    result = readStream.ReadToEnd();
                    readStream.Close();
                    readStream.Dispose();
                }
            }
            catch { }

            return result;
        }
    }
}
