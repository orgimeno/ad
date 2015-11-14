using System;
using Gtk;

using SerpisAd;
using PCategoria;
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
			new CategoriaView(null);
		};

		refreshAction.Activated += delegate {
			fill();
		};

		removeAction.Activated += delegate {
			object id=TreeViewHelper.GetId(treeView);
			//Console.Write("{0}", row[0]);
			if(TreeViewHelper.ConfirmDelete(this))
				eraseArticulo(id);
		};

		editAction.Activated += delegate{
			new CategoriaView(TreeViewHelper.GetId(treeView));
		};

		treeView.Selection.Changed += delegate {
			removeAction.Sensitive= TreeViewHelper.GetId(treeView) != null;
		};

		removeAction.Sensitive = false;

		//newAction.Activated += newActionActivated;
	}

	protected void fill(){
		QueryResult queryResult1 = PersisterHelper.Get ("select * from categoria");
		TreeViewHelper.Fill (treeView, queryResult1);
	}
	//	void newActionActivated (object sender, EventArgs e)
	//	{
	//		new ArticuloView ();
	//	}


	protected void eraseArticulo(object id){
		IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = "delete from categoria " +
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
