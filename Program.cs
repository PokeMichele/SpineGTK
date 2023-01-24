using System;
using System.IO;
using Gtk;
using System.Linq;
using System.Xml;

namespace SpineGTK_v1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();

            string home = Environment.GetEnvironmentVariable("HOME");
            if (string.IsNullOrEmpty(home))
            {
                var passwd = File.ReadAllLines("/etc/passwd");
                var entry = passwd.FirstOrDefault(x => x.StartsWith(Environment.UserName + ":"));
                if (entry != null)
                {
                    var parts = entry.Split(':');
                    home = parts[5];
                }
            }

            String Path = home + "/SpineGTK/config.xml";

            if (!Directory.Exists(home + "/SpineGTK"))
            {
                Directory.CreateDirectory(home + "/SpineGTK");
            }

            win.ShowAll();

            Application.Run();
        }
    }
}