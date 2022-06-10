using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RockstarsIT.Models
{
    public class Spotify
    {
        readonly SpotifyCredentials credentials = new SpotifyCredentials();

        public string GetAccessToken()
        {
            string url5 = "https://accounts.spotify.com/api/token";
            var clientid = credentials.ClientId;
            var clientsecret = credentials.ClientSecret;
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
            string url = "https://open.spotify.com/embed-podcast/episode/" + GetSpotifyLinkId(spotifyUrl);
            return url;
        }

        public string GetPreviewLink(string spotifyLinkId)
        {
            string url5 = "https://api.spotify.com/v1/episodes/" + spotifyLinkId + "?market=NL";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Bearer " + GetAccessToken());
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

        public string GetDescription(string spotifyLinkId)
        {
            string url5 = "https://api.spotify.com/v1/episodes/" + spotifyLinkId + "?market=NL";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Bearer " + GetAccessToken());
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
            return parsed["description"].ToString();
        }
        public string GetTitle(string spotifyLinkId)
        {
            string url5 = "https://api.spotify.com/v1/episodes/" + spotifyLinkId + "?market=NL";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Bearer " + GetAccessToken());
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
            return parsed["name"].ToString();
        }
        
        public string GetShowDescription(string spotifyLinkId)
        {
            string url5 = "https://api.spotify.com/v1/shows/" + spotifyLinkId + "?market=NL";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Bearer " + GetAccessToken());
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
            return parsed["description"].ToString();
        }
        public string GetShowTitle(string spotifyLinkId)
        {
            string url5 = "https://api.spotify.com/v1/shows/" + spotifyLinkId + "?market=NL";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Bearer " + GetAccessToken());
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
            return parsed["name"].ToString();
        }

        public bool CheckLinkInput(string link)
        {
            if (link.Contains("https://open.spotify.com/episode/"))
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(link);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                request.Method = "HEAD";
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.ResponseUri.ToString().Contains("spotify.com"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Invalid Link");
            }
        }
        
        public bool CheckShowLinkInput(string link)
        {
            if (link.Contains("https://open.spotify.com/show/"))
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(link);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                request.Method = "HEAD";
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.ResponseUri.ToString().Contains("spotify.com"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Invalid Link");
            }
        }
    }
}