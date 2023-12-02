using System;
using System.Collections.Generic;
using System.Xml;
using Gtk;
namespace SpineGTK_v1
{
    public partial class RemoveGameWindow : Gtk.Window
    {
        private List<CheckButton> checkButtons = new List<CheckButton>();

        public RemoveGameWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            string home = Environment.GetEnvironmentVariable("HOME");
            String path = home + "/SpineGTK/config.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList nodeList = doc.SelectNodes("/Games/Game");

            int yOffset = 0;

            foreach (XmlNode node in nodeList)
            {
                XmlNode childNode = node.SelectSingleNode("Name");

                CheckButton checkButton = new CheckButton
                {
                    Label = childNode.InnerText
                };
                fixed2.Put(checkButton, 0, yOffset);  // Posiziona la CheckButton in base all'offset verticale
                checkButtons.Add(checkButton);

                yOffset += 30;
            }

            fixed2.ShowAll();
        }

        protected void OnBtnCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        protected void OnBtnDeleteClicked(object sender, EventArgs e)
        {
            string home = Environment.GetEnvironmentVariable("HOME");
            String path = home + "/SpineGTK/config.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            for (int i = 0; i < checkButtons.Count; i++)
            {
                if (checkButtons[i].Active)
                {
                    // Rimuovi il gioco dal file di configurazione
                    string gameName = checkButtons[i].Label;
                    XmlNode gameNode = doc.SelectSingleNode("/Games/Game[Name='" + gameName + "']");
                    if (gameNode != null)
                    {
                        gameNode.ParentNode.RemoveChild(gameNode);
                    }
                }
            }
            //Save Config Edits
            doc.Save(path);

            this.Destroy();

            MainWindow mainWindow = new MainWindow();
            mainWindow.UpdateMainWindow();
        }
    }
}
