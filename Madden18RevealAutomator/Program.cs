using System;
using System.IO;
using System.Net;
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

            var lastPartofUrl = response.ResponseUri.AbsolutePath.Split('/')[4];
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var sub = responseFromServer.Substring(1900, 65);

            if (sub.Contains("Elite"))
            {
                for (int i = 90; i < 99; i++)
                {
                    var contains = sub.Contains(i + " OVR");
                    if (contains)
                    {
                        if (MessageBox.Show(string.Format(
                                @"Congratulations!  You found {0}{0}{1}{0}{0} https://www.muthead.com/18/players/reveal/" +
                                lastPartofUrl, Environment.NewLine, sub, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Asterisk)) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("https://www.muthead.com/18/players/reveal/" +
                                                             lastPartofUrl);
                        }
                    }
                }
            }
            
        }
    }
}
