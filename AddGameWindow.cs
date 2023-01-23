using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using SpineGTK_v1;
using System.Net;
using System.Diagnostics;

namespace SpineGTK_v1
{
    public partial class AddGameWindow : Gtk.Window
    {
        public AddGameWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }

        protected void OnBtnEnter1Clicked(object sender, EventArgs e)
        {
            if (entry1.Text != "" && entry2.Text != "")
            {
                String path = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK/config.xml";

                XDocument Xdoc = new XDocument(new XElement("Games"));

                if (File.Exists(path))
                {
                    Xdoc = XDocument.Load(path);
                }
                else
                {
                    Xdoc = new XDocument(new XElement("Games"));
                }

                var existing = Xdoc.Descendants("Game").FirstOrDefault(m => m.Element("Name")?.Value == entry1.Text);

                if (existing != null) //name existed in xml
                {
                    existing.Element("Directory").Value = entry2.Text;
                }
                else
                {
                    XElement xml = new XElement("Game");
                    xml.Add(new XElement("Name", entry1.Text));
                    xml.Add(new XElement("Directory", entry2.Text));

                    if (Xdoc.Descendants().Count() > 0)
                    {
                        Xdoc.Descendants().First().Add(xml);
                    }
                    else
                    {
                        Xdoc.Add(xml);
                    }
                }

                Xdoc.Save(path);
            }
            else
            {
                Console.WriteLine("ERROR: All Text Fields must be filled.");
            }

            this.Destroy();
        }

        protected void OnBtnCancel1Clicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        protected void OnBtnInstallSpineClicked(object sender, EventArgs e)
        {
            //File to Download & Download Path
            string url = "https://download1654.mediafire.com/rqlparu2cfog/iu19u2ratkbbz92/spine-20220517.zip";
            string filePath = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/SpineGTK/";

            //Check if the directory exists
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                Console.WriteLine("Directory Created");
            }

            //Try to give permissions for the folder
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "/bin/bash";
                process.StartInfo.Arguments = $"-c \"chmod a+rw {filePath}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();
                process.WaitForExit();
                Console.WriteLine("Folder Permissions given successfully");
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Warning: Folder's permissions not set. Try executing this Software as ROOT");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: An exception occurred while trying to set folder's permissions: {0}", ex.Message);
            }

            //Download File
            string fileToDownload = filePath + "spine-20220517.zip";
            if (!File.Exists(fileToDownload))
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    using (Stream responseStream = response.GetResponseStream())
                    using (FileStream fileStream = File.Create(filePath + "spine-20220517.zip"))
                    {
                        responseStream.CopyTo(fileStream);
                    }

                    response.Close();
                }
                catch (WebException ex)
                {
                    Console.WriteLine("An error occurred while downloading the file: {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File already exists, skipping download.");
            }

            //UnZip File
            string zipPath = filePath + "spine-20220517.zip";
            string extractPath = filePath + "Spine/";

            if (!Directory.Exists(extractPath) || !Directory.GetFiles(extractPath).Any())
            {
                try
                {
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    Console.WriteLine("Zip File extracted successfully");
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Zip File Extraction Error: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Error: Target directory already exists and is not empty.");
            }
        }

    }
}
