using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;

using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Console.WriteLine ("MainWindow ctor.");
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		string[] columnNames = queryResult.ColumnNames;
		CellRendererText cellRendererText = new CellRendererText ();
		for (int index = 0; index < columnNames.Length; index++) {
			int column = index;
			treeView.AppendColumn (columnNames [index], cellRendererText, 
			                       delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row = (IList)tree_model.GetValue(iter, 0);
				cellRendererText.Text = row[column].ToString();
			});
		}
		ListStore listStore = new ListStore (typeof(IList));
		foreach (IList row in queryResult.Rows)
			listStore.AppendValues (row);
		treeView.Model = listStore;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
