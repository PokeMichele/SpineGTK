using System;
using System.IO;
using Gtk;
using System.Xml;

namespace SpineGTK_v1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();

            String Path = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK/config.xml";

            if (!Directory.Exists(Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK");
            }

            win.ShowAll();

            Application.Run();
        }
    }
}