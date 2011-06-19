using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NReadability;
using TidyManaged;
using System.Net;
using Twitter2Reader.LalaDataSetTableAdapters;
using System.IO;
using TweetSharp;

namespace Twitter2Reader
{
    class Utils
    {
        public static List<string> PostToReader(string _consumerKey, string _consumerSecret, string _accessToken, string _accessTokenSecret, string gUsername, string gPassword, bool chkTweets, bool chkFavorites, bool chkRetweets,  bool chkLinks, bool chkNoLinks)
        {
            List<string> res = new List<string>();
            GoogleReader gr = new GoogleReader(gUsername, gPassword);
            var serviceReader = new TwitterService(_consumerKey, _consumerSecret);
            serviceReader.AuthenticateWith(_accessToken, _accessTokenSecret);
            Regex urlfind = new Regex("((https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[A-Z0-9+&@#/%=~_|])", RegexOptions.IgnoreCase);

            List<TwitterStatus> mytweets =null;
            List<TwitterStatus> favorites=null;
            List<TwitterStatus> retweets =null;
            if (chkTweets)    mytweets  = serviceReader.ListTweetsOnUserTimeline(5).ToList();
            if (chkFavorites) favorites = serviceReader.ListFavoriteTweets(5).ToList();
            if (chkRetweets)  retweets  = serviceReader.ListRetweetsByMe(5).ToList();

            List<TwitterStatus> alltweets = new List<TwitterStatus>();
            if (mytweets != null) alltweets.AddRange(mytweets);
            if (favorites != null) alltweets.AddRange(favorites);
            if (retweets != null) alltweets.AddRange(retweets);
            
            foreach (var tweet in alltweets)
            {
                if (hassent(tweet.Id.ToString())) { continue; }
                Match thematch = urlfind.Match(tweet.Text);
                if (thematch.Success)
                {
                    if (chkLinks)
                    {
                        string theshorturl = thematch.Groups[1].Value;
                        string theurl = thematch.Groups[1].Value;
                        try
                        {
                            string longurlxml = c.DownloadString("http://api.unshort.me/?r=" + theurl);
                            Regex resUrl = new Regex("<resolvedURL>(.*?)</resolvedURL>");
                            Match resolved = resUrl.Match(longurlxml);
                            if (resolved.Success)
                            {
                                theurl = resolved.Groups[1].Value;
                            }
                        }
                        catch
                        {

                        }
                        string title;
                        string content = embed(theurl, out title);
                        gr.post(content, theurl, title, tweet.Text.Replace(theshorturl, "").Trim());
                        AddID(tweet.Id.ToString(), tweet.Text);
                    }

                }
                else if (chkNoLinks)
                {
                    gr.post("", "http://twitter.com/" + tweet.User.ScreenName + "/status/" + tweet.Id, "from twitter", tweet.Text.Trim());
                    AddID(tweet.Id.ToString(), tweet.Text);

                }
                res.Add( DateTime.Now.ToShortTimeString() + ": " + tweet.Text);
            }
            return res;
        }

        private static void AddID(string id, string theItem)
        {

            UsedIDsTableAdapter s = new UsedIDsTableAdapter();
            s.Insert(id, theItem);
        }


        private static string embed(string url, out string title)
        {
            title = url;
            Console.WriteLine("Embeding " + url);
            if (url.Contains("youtube.com/"))
            {
                string vidid = "";
                Regex s = new Regex(@"^.*((youtu.be\/)|(v\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*");
                Match ss = s.Match(url);
                if (ss.Success)
                {
                    vidid = ss.Groups[6].Value;
                    return " <iframe width=\"560\" height=\"349\" src=\"http://www.youtube.com/embed/" + vidid + "\" frameborder=\"0\" allowfullscreen></iframe> ";

                }
            }

            string embedlyurl = "http://api.embed.ly/1/oembed?url=" + url;
            string embedoutput = "";
            try
            {
                embedoutput = c.DownloadString(embedlyurl);
            }
            catch (Exception e)
            {

            }
            Regex html = new Regex(@"""html"": ""(.*?)\"",");
            Regex urlre = new Regex(@"""url"": ""(.*?)\"",.*?""width"": (.*?),");
            Regex type = new Regex(@"""type"": ""(.*?)""");
            Regex titles = new Regex(@"""title"": ""(.*?)""");
            Match embtype = type.Match(embedoutput);
            if (embtype.Success)
            {
                Match embtitle = titles.Match(embedoutput);
                if (embtitle.Success)
                {
                    title = embtitle.Groups[1].Value;
                }

                if (embtype.Groups[1].Value == "photo")
                {
                    Match emburl = urlre.Match(embedoutput);
                    if (emburl.Success)
                    {
                        string width = emburl.Groups[2].Value;
                        int iwidth = 0;
                        int.TryParse(width, out iwidth);
                        if (iwidth > 700)
                        {
                            return "<img src=\"" + emburl.Groups[1].Value + "\" width=\"700\"/>";
                        }
                        else
                        {
                            return "<img src=\"" + emburl.Groups[1].Value + "\"/>";
                        }
                    }
                }
                else
                {
                    Match embhtml = html.Match(embedoutput);
                    if (embhtml.Success)
                    {
                        return embhtml.Groups[1].Value;
                    }
                }

            }
            try
            {

                // string realhtml = c.DownloadString(url);
                //NReadabilityTranscoder rd = new NReadabilityTranscoder();
                NReadabilityWebTranscoder rdw = new NReadabilityWebTranscoder();

                bool extracted = false;

                // string transcoded = rd.Transcode(realhtml, out extracted);
                string transcoded = rdw.Transcode(url, out extracted);
                if (extracted)
                {
                    Regex body = new Regex("<body>(.*?)</body>", RegexOptions.Singleline);
                    Match bodym = body.Match(transcoded);
                    if (bodym.Success)
                    {
                        transcoded = "<html><body>" + bodym.Groups[1].Value + "</body></html>";
                    }
                    Regex header = new Regex("(<h1>.*?</h1>)", RegexOptions.Singleline);
                    transcoded = header.Replace(transcoded, "");
                    string realhtml = c.DownloadString(url);
                    Regex regexs = new Regex("<title>(.*?)</title>",
                        RegexOptions.IgnoreCase);
                    Match match = regexs.Match(realhtml);
                    if (match.Success)
                    {
                        title = match.Groups[1].Value;
                    }
                    return transcoded;
                }
            }
            catch (Exception e)
            {
                try
                {
                    string realhtml = c.DownloadString(url);
                    Regex regexs = new Regex(".*<head>.*<title>(.*)</title>.*</head>.*",
                        RegexOptions.IgnoreCase);
                    Match match = regexs.Match(realhtml);
                    if (match.Success)
                    {
                        title = match.Groups[0].Value;
                    }
                    realhtml = SanitizeXmlString(realhtml);
                    bool extracted = false;

                    NReadabilityTranscoder rd = new NReadabilityTranscoder();
                    //NReadabilityWebTranscoder rdw = new NReadabilityWebTranscoder();


                    // string transcoded = rd.Transcode(realhtml, out extracted);
                    string transcoded = rd.Transcode(realhtml, url, out extracted);
                    if (extracted)
                    {
                        Regex body = new Regex("<body>(.*?)</body>", RegexOptions.Singleline);
                        Match bodym = body.Match(transcoded);
                        if (bodym.Success)
                        {
                            transcoded = "<html><body>" + bodym.Groups[1].Value + "</body></html>";
                        }
                        Regex header = new Regex("(<h1>.*?</h1>)", RegexOptions.Singleline);
                        transcoded = header.Replace(transcoded, "");
                        return transcoded;
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        string realhtml = SanitizeXmlString(c.DownloadString(url));
                        Regex regexs = new Regex(".*<head>.*<title>(.*)</title>.*</head>.*",
                          RegexOptions.IgnoreCase);
                        Match match = regexs.Match(realhtml);
                        if (match.Success)
                        {
                            title = match.Groups[0].Value;
                        }
                        bool extracted = false;
                        using (Document doc = Document.FromString(realhtml))
                        {
                            doc.ShowWarnings = false;
                            doc.Quiet = true;
                            doc.OutputXhtml = true;
                            doc.CleanAndRepair();
                            realhtml = doc.Save();
                        }
                        NReadabilityTranscoder rd = new NReadabilityTranscoder();
                        //NReadabilityWebTranscoder rdw = new NReadabilityWebTranscoder();


                        // string transcoded = rd.Transcode(realhtml, out extracted);
                        string transcoded = rd.Transcode(realhtml, url, out extracted);
                        if (extracted)
                        {
                            Regex body = new Regex("<body>(.*?)</body>", RegexOptions.Singleline);
                            Match bodym = body.Match(transcoded);
                            if (bodym.Success)
                            {
                                transcoded = "<html><body>" + bodym.Groups[1].Value + "</body></html>";
                            }
                            Regex header = new Regex("(<h1>.*?</h1>)", RegexOptions.Singleline);
                            transcoded = header.Replace(transcoded, "");
                            return transcoded;
                        }

                    }
                    catch (Exception exx)
                    {

                    }

                }
            }
            return "";

        }

        static WebClient c = new WebClient();

        private static bool hassent(string id)
        {
            UsedIDsTableAdapter s = new UsedIDsTableAdapter();

            return s.GetID(id) > 0;
        }


        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        public static string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            StringBuilder buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }

    }

    public class GoogleReader
    {
        private string _sid = null;
        private string _token = null;
        private string _auth = null;

        private string _username;
        private string _password;

        public void post(string content, string noteurl, string title, string annotation)
        {
            string url = "http://www.google.co.uk/reader/api/0/item/edit";
            string sparams = "snippet=" + content + "&T=" + _token + "&share=true" + "&url=" + noteurl + "&title=" + title + "&annotation=" + annotation;
            byte[] postData = UTF8Encoding.UTF8.GetBytes(sparams);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers.Add("Authorization: GoogleLogin auth=" + _auth);
            req.Method = "POST";
            req.AllowWriteStreamBuffering = true;
            req.ContentLength = postData.Length;
            using (var dataStream = req.GetRequestStream())
            {
                dataStream.Write(postData, 0, postData.Length);
            }
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string ret = "Nothing";
            using (var stream = response.GetResponseStream())
            {
                StreamReader r = new StreamReader(stream);
                ret = r.ReadToEnd();
            }
            Console.WriteLine(ret);
        }

        public GoogleReader(string username, string password)
        {
            _username = username;
            _password = password;

            connect();
        }

        private bool connect()
        {
            getToken();
            return _token != null;
        }

        private void getToken()
        {
            getSid();
            //        _cookie = new Cookie("SID", _sid, "/", ".google.com");

            string url = "http://www.google.com/reader/api/0/token";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Headers.Add("Authorization: GoogleLogin auth=" + _auth);
            //          req.CookieContainer = new CookieContainer();
            //            req.CookieContainer.Add(_cookie);

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                StreamReader r = new StreamReader(stream);
                _token = r.ReadToEnd();
            }
        }

        private void getSid()
        {
            string requestUrl = string.Format
                ("https://www.google.com/accounts/ClientLogin?service=reader&Email={0}&Passwd={1}",
                _username, _password);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUrl);
            req.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                StreamReader r = new StreamReader(stream);
                string resp = r.ReadToEnd();

                int indexSid = resp.IndexOf("SID=") + 4;
                int indexLsid = resp.IndexOf("LSID=");
                int indexAuth = resp.IndexOf("Auth=");
                _sid = resp.Substring(indexSid, indexLsid - 5);
                _auth = resp.Substring(indexAuth + 5);
            }
        }
    }
}
