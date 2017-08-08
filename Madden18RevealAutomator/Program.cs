using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            //1900-1970
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(
                "https://www.muthead.com/18/players/reveal/"); //reveal would be split[5]
            // Get the response.  
            WebResponse response = request.GetResponse();
            var uri = response.ResponseUri.AbsolutePath;
            var split = uri.Split('/');
            var lastPartofUrl = split[4];
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            var sub = responseFromServer.Substring(1900, 65);
            if (!sub.Contains("Elite"))
            {
                return;
            }
            for (int i = 90; i < 99; i++)
            {
                var contains = sub.Contains(i + " OVR");
                if (!contains)
                {
                    continue;
                }
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
