using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Madden18RevealAutomator
{
    class Program
    {
        private static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                count++;
                DoWork();
            }
        }

        public static void DoWork()
        {
            WebRequest request = WebRequest.Create(
                "https://www.muthead.com/18/players/reveal/");
            WebResponse response = request.GetResponse();

            var uriSplit = response.ResponseUri.AbsoluteUri.Split('/');
            var lastPartofUrl = uriSplit[4];
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var subb = Regex.Match(responseFromServer, @"<title>(.*?)</title>");
            var splitsub = subb.ToString().Substring(7, subb.Length - 16).Split('-');
            var sub = splitsub[2].Substring(0, 8) + splitsub[0];

            if (sub.Contains("Elite"))
            {
                for (int i = 90; i < 99; i++)
                {
                    var contains = sub.Contains(i + " OVR");
                    if (contains)
                    {
                        MessageBox.Show(string.Format(
                            @"Congratulations!  You found {0}{0}{1}{0}{0} https://www.muthead.com/18/players/reveal/" +
                            lastPartofUrl, Environment.NewLine, sub));
                    }
                }
            }
        }
    }
}
