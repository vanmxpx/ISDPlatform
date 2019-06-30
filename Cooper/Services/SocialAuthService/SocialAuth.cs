using Cooper.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cooper.Services
{
    class SocialAuth : ISocialAuth
    {
        readonly string FacebookAppID, FacebookSecretKey;
        readonly string getFacebookSecretKey;

        public SocialAuth(IConfigProvider configProvider) {
            this.FacebookAppID = configProvider.FacebookProvider.AppID;
            this.FacebookSecretKey = configProvider.FacebookProvider.AppSecretKey;

            getFacebookSecretKey = $"https://graph.facebook.com/oauth/access_token?client_id={FacebookAppID}&client_secret={FacebookSecretKey}&grant_type=client_credentials";
        }

        public bool IsFacebookAuth(string token, string user_id) {
            string secretKey = GetFacebookSecretKey();
            string url = $"https://graph.facebook.com/debug_token?input_token={token}&access_token={secretKey}";
            var result = JObject.Parse(ParseURL(url));
            return (bool)(result.SelectToken("data").SelectToken("is_valid")) 
                && (string)(result.SelectToken("data").SelectToken("user_id")) == user_id;
        }

        public bool IsGoogleAuth(string idToken, string email) {
            string url = $"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}";
            var result = JObject.Parse(ParseURL(url));
            return (string)(result.SelectToken("error")) == null
                && (bool)(result.SelectToken("email_verified"))
                && (string)(result.SelectToken("email")) == email;
        }

        private string GetFacebookSecretKey() {
            return (string)(JObject.Parse(ParseURL(getFacebookSecretKey)).SelectToken("access_token"));
        }

        private string ParseURL(string url) {
            string result = null;
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) {
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
            } catch { }

            return result;
        }
    }
}