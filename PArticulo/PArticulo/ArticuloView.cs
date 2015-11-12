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
				entryNombre.Text=queryResult.Rows[1];
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

		}

	}
}

