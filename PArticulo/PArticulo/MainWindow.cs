using System;
using Gtk;
using MySql.Data.MySqlClient;
public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.Write ("hola mundo");
		/*
		MySqlConnection mySqlConnection = new MySqlConnection ("Database=dbprueba;Data Source=localhost;User id=root;Password=sistemas");
		mySqlConnection.Open();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();

		mySqlCommand.CommandText="select id, nombre from articulo";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		while (mySqlDataReader.Read()) {
			Console.WriteLine ("id={0} nombre={1}", mySqlDataReader[0], mySqlDataReader[1]);
		}

		mySqlDataReader.Close ();
		mySqlConnection.Close ();
		*/
		treeView.AppendColumn ("id", new CellRendererText(),"text",0);
		treeView.AppendColumn ("nombre", new CellRendererText(),"text",1);
		ListStore liststore = new ListStore (typeof(long), typeof(String));

		liststore.AppendValues ("1l", "nombre del primero");
		treeView.Model = liststore;
		liststore.AppendValues ("2l", "nombre del segundo");
	}	

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
