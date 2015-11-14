using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PCategoria
{
	public partial class CategoriaView : Gtk.Window
	{
		public CategoriaView (object id) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			if (id==null) {
				saveAction.Activated += delegate {
					save ();
				};
				//Si hay que actualizar el articulo
			} else {
				QueryResult queryResult = PersisterHelper.Get ("select * from categoria where id="+id);
				foreach (var row in queryResult.Rows) {
					entryNombre.Text = row [1].ToString ();
				}
				saveAction.Activated += delegate {
					updateArt (id);
				};
			}

		}

		private void save() {

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into categoria (nombre) " +
				"values (@nombre)";

			string nombre = entryNombre.Text;

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private void updateArt(object id){

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update categoria set nombre=@nombre"+
				" where id=@id"; 

			string nombre = entryNombre.Text;

			DbCommandHelper.AddParameter (dbCommand, "id", id);
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			dbCommand.ExecuteNonQuery ();
			Destroy ();

		}

	}
}
