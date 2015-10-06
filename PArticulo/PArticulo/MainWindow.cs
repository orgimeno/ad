using System;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("MainWindow ctor.");
		MySqlConnection mySqlConnection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
		);
		mySqlConnection.Open ();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from articulo";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		ListStore listStore = new ListStore (typeof(String), typeof(String), typeof(String), typeof(String));

		while (mySqlDataReader.Read()) {
			Console.WriteLine ("id={0} nombre={1}", mySqlDataReader [0], mySqlDataReader [1]);
			listStore.AppendValues (mySqlDataReader [0].ToString(), mySqlDataReader [1], mySqlDataReader [2].ToString(), mySqlDataReader[3].ToString());
		}


		mySqlDataReader.Close ();
		mySqlConnection.Close ();

		//añado columnas
		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		treeView.AppendColumn ("categoria", new CellRendererText (), "text", 2);
		treeView.AppendColumn ("precio", new CellRendererText (), "text", 3);

		//establezco el modelo
		ListStore listStore1 = new ListStore (typeof(String), typeof(String));
		//TODO rellenar listStore


		listStore.AppendValues ("1", "Nombre del primero");

		treeView.Model = listStore;

		string[] values = new string[2];
		values [0] = "2";
		values [1] = "Nombre del segundo";
		listStore.AppendValues (values);



	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
