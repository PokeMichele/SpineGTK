using System;
using Gtk;
using SpineGTK_v1;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        String path = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK/config.xml";
        XmlDocument doc = new XmlDocument();
        XDocument Xdoc = new XDocument(new XElement("Games"));

        if (File.Exists(path))
        {
            Xdoc = XDocument.Load(path);
            doc.Load(path);
        }
        else
        {
            Xdoc = new XDocument(new XElement("Games"));
        }


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



    private void OnButtonClicked(object sender, EventArgs e)
    {
        Button clickedButton = sender as Button;
        string gameName = clickedButton.Label;

        // Try to give permissions for the executable file
        try
        {
            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"chmod a+rwx {Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile)}/SpineGTK/Spine/20220517/spine\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();
            process.WaitForExit();
            Console.WriteLine("File Permissions given successfully");
            if (process.ExitCode != 0)
            {
                Console.WriteLine("Warning: File permissions not set. Try executing this Software as ROOT");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: An exception occurred while trying to set file's permissions: {0}", ex.Message);
        }

        // Shell command to run the game
        String path = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK/config.xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode gameNode = doc.SelectSingleNode("/Games/Game[Name='" + gameName + "']");
        if (gameNode != null)
        {
            XmlNode directoryNode = gameNode.SelectSingleNode("Directory");
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "/bin/bash";
                process.StartInfo.Arguments = $"-c \"{Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile)}/SpineGTK/Spine/20220517/spine {directoryNode.InnerText}\"";
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