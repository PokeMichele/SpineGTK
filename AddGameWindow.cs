using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using SpineGTK_v1;

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
    }
}
