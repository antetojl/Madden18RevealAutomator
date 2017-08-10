using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Madden18RevealAutomator
{
    class Program
    {
        //this no longer works because the reveal page is gone!  Should be good for Madden 19 tho :)
        private static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                count++;
                CheckForElites();
            }
        }

        /// <summary>
        /// WebRequest used to keep requesting reveal page, getting new page everytime.  Looks for certain attribues in title of page.  
        /// MessageBox appears if player of desired overall is found.
        /// </summary>
        public static void CheckForElites()
        {
            //make request
            WebRequest request = WebRequest.Create(
                "https://www.muthead.com/18/players/reveal/");
            WebResponse response = request.GetResponse();

            //split url
            string[] uriSplit = response.ResponseUri.AbsoluteUri.Split('/');

            //get last section of URL
            string lastPartofUrl = uriSplit[4];

            //read HTML
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            //check for matches
            var match = Regex.Match(responseFromServer, @"<title>(.*?)</title>");
            
            //at least they're 80 OVR...
            if (match.ToString().Contains("Elite"))
            {
                //create substring in "99 OVR - Player name" format
                var splitMatch = match.ToString().Substring(7, match.Length - 16).Split('-');
                var OVRandName = splitMatch[2].Substring(0, 8) + splitMatch[0];

                for (int i = 90; i < 99; i++)
                {
                    var contains = OVRandName.Contains(i + " OVR");
                    if (contains)
                    {
                        MessageBox.Show(string.Format(
                            @"Congratulations!  You found {0}{0}{1}{0}{0} https://www.muthead.com/18/players/reveal/" +
                            lastPartofUrl, Environment.NewLine, OVRandName));
                    }
                }
            }
        }
    }
}
