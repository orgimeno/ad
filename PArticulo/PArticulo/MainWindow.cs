using System;
using Gtk;

using SerpisAd;
using PArticulo;
using System.Collections;
using System.Data;

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

		removeAction.Activated += delegate {
			TreeIter treeIter;
			treeView.Selection.GetSelected(out treeIter);
			IList row= (IList)treeView.Model.GetValue(treeIter, 0);
			Console.Write("{0}", row[0]);
			eraseArticulo(row[0]);

		};

		editAction.Activated += delegate{
			TreeIter treeIter;
			treeView.Selection.GetSelected(out treeIter);
			IList row= (IList)treeView.Model.GetValue(treeIter, 0);
			Console.Write("{0}", row[0]);
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

	protected void eraseArticulo(object id){
		IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = "delete from articulo " +
				"where id=@id";

		DbCommandHelper.AddParameter (dbCommand, "id", id);
		Console.Write (id+" id");
		dbCommand.ExecuteNonQuery ();
		fill ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}
