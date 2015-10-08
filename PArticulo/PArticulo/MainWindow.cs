using System;
using System.Data;
using System.Collections.Generic;
using Gtk;
using MySql.Data.MySqlClient;
using PArticulo;
public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");
		IDbConnection dbConnection = App.Instance.DbConnection;
		MySqlConnection mySqlConnection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
			);
		mySqlConnection.Open ();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from articulo";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();
		//		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		//		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		string[] columnNames = getColumnNames (mySqlDataReader);
		for (int index = 0; index < columnNames.Length; index++)
			treeView.AppendColumn (columnNames [index], new CellRendererText (), "text", index);

		//ListStore listStore = new ListStore (typeof(String), typeof(String));
		Type[] types = getTypes (mySqlDataReader.FieldCount);
		ListStore listStore = new ListStore (types);

		while (mySqlDataReader.Read()) {
			//listStore.AppendValues (mySqlDataReader [0].ToString(), mySqlDataReader [1].ToString());
			string[] values = getValues (mySqlDataReader);
			listStore.AppendValues (values);
		}

		mySqlDataReader.Close ();

		treeView.Model = listStore;

		mySqlConnection.Close ();
	}

	private string[] getColumnNames(MySqlDataReader mySqlDataReader) {
		List<string> columnNames = new List<string> ();
		int count = mySqlDataReader.FieldCount;
		for (int index = 0; index < count; index++)
			columnNames.Add (mySqlDataReader.GetName (index));
		return columnNames.ToArray ();
	}

	private Type[] getTypes(int count) {
		List<Type> types = new List<Type> ();
		for (int index = 0; index < count; index++)
			types.Add (typeof(string));
		return types.ToArray ();
	}

	private string[] getValues(MySqlDataReader mySqlDataReader) {
		List<string> values = new List<string> ();
		int count = mySqlDataReader.FieldCount;
		for (int index = 0; index < count; index++)
			values.Add (mySqlDataReader [index].ToString ());
		return values.ToArray ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
