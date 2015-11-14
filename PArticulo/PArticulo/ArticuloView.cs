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
			//Si es un articulo nuevo
			if (id==null) {
				QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
				ComboBoxHelper.Fill (comboBoxCategoria, queryResult);
			
				saveAction.Activated += delegate {
					save ();
				};
			//Si hay que actualizar el articulo
			} else {
				QueryResult queryResult = PersisterHelper.Get ("select * from articulo where id="+id);
				foreach (var row in queryResult.Rows) {
					entryNombre.Text = row [1].ToString ();
					QueryResult queryResultCat = PersisterHelper.Get ("select * from categoria");
					ComboBoxHelper.Fill (comboBoxCategoria, queryResultCat);
					comboBoxCategoria.Active = Int32.Parse(row [2].ToString());
					spinButtonPrecio.Text = row [3].ToString ();
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

