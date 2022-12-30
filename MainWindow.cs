using System;
using Gtk;
using SpineGTK_v1;
using System.Xml;
using System.Xml.Linq;


public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
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