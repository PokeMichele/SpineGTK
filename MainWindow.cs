using System;
using Gtk;
using SpineGTK_v1;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        String path = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK/config.xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        XmlNodeList nodeList = doc.SelectNodes("/Games/Game");
        VBox vbox = new VBox();
        vbox.Spacing = 6;
        foreach (XmlNode node in nodeList)
        {
            XmlNode childNode = node.SelectSingleNode("Name");

            Button btn = new Button(childNode.InnerText);
            btn.SetAlignment(0, 0);
            btn.Clicked += OnButtonClicked;
            vbox.Add(btn);
        }
        fixed1.Add(vbox);
    }

    //Each Button Function
    private void OnButtonClicked(object sender, EventArgs e)
    {
        String path = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK/config.xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList nodeList = doc.SelectNodes("/Games/Game");
        foreach (XmlNode node in nodeList)
        {
            XmlNode childNode = node.SelectSingleNode("Directory");

            //Shell Command
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "/bin/bash";
                process.StartInfo.Arguments = "-c \"spine " + childNode.InnerText + "\""; //bash -c spine /your/game/directory/   PLEASE NOTE: Spine must be installed
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
    }


    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnAddGameClicked(object sender, EventArgs e)
    {
        AddGameWindow addGameWindow = new AddGameWindow();
        addGameWindow.Show();

    }
}