using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TweetSharp;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Linq.Expressions;

namespace Twitter2Reader
{
    public partial class frmTweet2Reader : Form
    {
        public bool running = false;
        public frmTweet2Reader()
        {
            InitializeComponent();
        }

        private static string _consumerKey = "pgLtkJYzQCwPei6botJAjw";
        private static string _consumerSecret = "cyMGWzF2XEZ65i3al4KRMXmNhh97rIcbGOaf6vIKWY0";
        private static string _accessToken = "";
        private static string _accessTokenSecret = "";
        private static string _gUsername = "";
        private static string _gPassword = "";
        private static int interval;

        List<string> settings = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            var serviceauth = new TwitterService(_consumerKey, _consumerSecret);
            OAuthRequestToken unauthorizedToken = serviceauth.GetRequestToken();
            string url = serviceauth.GetAuthorizationUri(unauthorizedToken).AbsoluteUri;

            Process.Start(url); 

            using (frmAskForPin pinfrm = new frmAskForPin())
            {
                pinfrm.ShowDialog();
                if (pinfrm.pin != "")
                {

                    OAuthAccessToken accessToken = serviceauth.GetAccessToken(unauthorizedToken, pinfrm.pin);
                    settings[0] =accessToken.ScreenName + " " + accessToken.Token + " " + accessToken.TokenSecret;
                    File.WriteAllLines("settings.ini", settings);
                    listBox1.Items.Insert(0, "Authenticated: " + accessToken.ScreenName);
                }
            }
        }

        private void frmTwitter2Reader_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3 - settings.Count; i++)
            {
                settings.Add("");
            }

            if (File.Exists("settings.ini"))
            {
                settings = File.ReadAllLines("settings.ini").ToList();
                for (int i = 0; i < 3 - settings.Count; i++)
                {
                    settings.Add("");
                }
                if (settings[0] != "")
                {
                    string[] twitter = settings[0].Split(' ');
                    label4.Text = twitter[0];
                    _accessToken = twitter[1];
                    _accessTokenSecret = twitter[2];
                }
                if (settings[1] != "")
                {
                    string[] greader = settings[1].Split(' ');
                    _gUsername = greader[0];
                    _gPassword = greader[1];
                    textBox1.Text = _gUsername;
                    textBox2.Text = _gPassword;
                }
                if (settings[2] != "")
                {
                    string[] sets = settings[2].Split(' ');
                    interval = int.Parse(sets[0]);
                    label5.Text = interval.ToString();
                    trackBar1.Value = interval;
                    chkTweets.Checked = (sets[1] == "True");
                    chkFavorites.Checked = (sets[2] == "True");
                    chkRetweets.Checked = (sets[3] == "True");
                    chkLinks.Checked = (sets[4] == "True");
                    chkNoLinks.Checked = (sets[5] == "True");
                    running = true;
                    ThreadPool.QueueUserWorkItem(delegate { Go(); });
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (!running)
            {
                running = true;
                ThreadPool.QueueUserWorkItem(delegate { Go(); });

            }

        }

        private void Go()
        {           
            while (running)
            {
                try
                {

                    List<string> res = Utils.PostToReader(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret, _gUsername, _gPassword, chkTweets.Checked, chkFavorites.Checked, chkRetweets.Checked, chkLinks.Checked, chkNoLinks.Checked);
                    this.Invoke((MethodInvoker)delegate { listBox1.Items.AddRange(res.ToArray()); });
                    this.Invoke((MethodInvoker)delegate { listBox1.Items.Add(DateTime.Now.ToString() + ": Done"); });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate { listBox1.Items.Add(DateTime.Now.ToShortTimeString() + ": " + ex.Message); });
                }
                Thread.Sleep(interval * 60000);
            
            }
        }

        private void SaveSettings()
        {
            settings[1] = textBox1.Text + " " + textBox2.Text;
            settings[2] = trackBar1.Value.ToString() + " " + chkTweets.Checked.ToString() + " " + chkFavorites.Checked.ToString() + " " + 
                chkRetweets.Checked.ToString() + " " + chkLinks.Checked.ToString() + " " + chkNoLinks.Checked.ToString();
            File.WriteAllLines("settings.ini", settings);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            interval = trackBar1.Value;
            label5.Text = trackBar1.Value.ToString();
        }


    }
}
