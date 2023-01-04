using System;
using Gtk;
using SpineGTK;

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
        Window addGameWindow = new Add_Game();
        addGameWindow.Show();
    }
}
