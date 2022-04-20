using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RockstarsIT.Models
{
    public class Spotify
    {
        private string _clientId = "f5bfb02b6b684aa0a51059c31b4fd0f2";
        private string _clientSecret = "a9f624e83e96464b9685ce5cbcbf19c2";
        
        public string GetAccessToken()
        {
            string url5 = "https://accounts.spotify.com/api/token";
            var clientid = _clientId;
            var clientsecret = _clientSecret;
            //request to get the access token
            var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientid, clientsecret)));
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);
            var request = ("grant_type=client_credentials");
            byte[] req_bytes = Encoding.ASCII.GetBytes(request);
            webRequest.ContentLength = req_bytes.Length;
            Stream strm = webRequest.GetRequestStream();
            strm.Write(req_bytes, 0, req_bytes.Length);
            strm.Close();
            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            var parsed = JObject.Parse(json);
            return parsed["access_token"].ToString();
        }

        public string GetSpotifyLinkId(string spotifyUrl)
        {
            Uri uri = new UriBuilder(spotifyUrl).Uri;
            string spotifyLinkId = uri.Segments[2];
            return spotifyLinkId;
        }
        
        public string GetSpotifyEmbeddedLink(string spotifyUrl)
        {
            string url = "https://open.spotify.com/embed-podcast/episode/"+GetSpotifyLinkId(spotifyUrl);
            return url;
        }
        
        public string GetPreviewLink(string spotifyLinkId)
        {
            string url5 = "https://api.spotify.com/v1/episodes/"+spotifyLinkId+"?market=NL";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Bearer "+GetAccessToken());
            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            var parsed = JObject.Parse(json);
            return parsed["audio_preview_url"].ToString();
        }
    }
}