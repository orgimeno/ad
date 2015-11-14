using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView (object id) : 
			base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			if (id==null) {
				//entryNombre.Text = "nuevo";
				QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
				ComboBoxHelper.Fill (comboBoxCategoria, queryResult);
				//spinButtonPrecio.Value = 1.5;

				saveAction.Activated += delegate {
					save ();
				};
			} else {
				QueryResult queryResult = PersisterHelper.Get ("select * from articulo where id="+id);
				foreach (var p in queryResult.Rows) {
					entryNombre.Text = p [1].ToString ();
					QueryResult queryResultCat = PersisterHelper.Get ("select * from categoria");
					ComboBoxHelper.Fill (comboBoxCategoria, queryResultCat);
					spinButtonPrecio.Text = p [3].ToString ();
				}
				saveAction.Activated += delegate {
					updateArt (id);
				};
			}

		}

		private void save() {

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			string nombre = entryNombre.Text;
			object categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			decimal precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private void updateArt(object id){

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre=@nombre, categoria=@categoria, precio=@precio"+
				" where id=@id"; 

			string nombre = entryNombre.Text;
			object categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			decimal precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter (dbCommand, "id", id);
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();

		}

	}
}

