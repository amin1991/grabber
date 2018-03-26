using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ImageGrabber
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            WebClient w = new WebClient();
            string s = w.DownloadString("http://readberserk.com/chapter/berserk-chapter-a0/");

            foreach (string i in LinkFinder.Find(s))
            {
                Debug.WriteLine(i);
            }

            MessageBox.Show("done");
        }

        static class LinkFinder
        {
            public static List<string> Find(string file)
            {
                List<string> list = new List<string>();

                // 1.
                // Find all matches in file.
                MatchCollection m1 = Regex.Matches(file, @"(<img.*?>)", RegexOptions.Singleline);

                // 2.
                // Loop over each match.
                foreach (Match m in m1)
                {
                    string value = m.Groups[1].Value;

                    // 3.
                    // Get href attribute.
                    Match m2 = Regex.Match(value, @"src=\""(.*?)\""", RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        list.Add(m2.Groups[1].Value);
                    }
                }
                return list;
            }
        }
    }
}
