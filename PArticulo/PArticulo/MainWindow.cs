using System;
using Gtk;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Console.WriteLine ("MainWindow ctor.");
		fill ();
		newAction.Activated += delegate {
			new ArticuloView();
		};

		refreshAction.Activated += delegate {
			fill();
		};


		//newAction.Activated += newActionActivated;
	}

	protected void fill(){
		QueryResult queryResult1 = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult1);
	}
//	void newActionActivated (object sender, EventArgs e)
//	{
//		new ArticuloView ();
//	}
	

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}
