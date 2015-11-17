using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private object id;
		//Si es un articulo nuevo
		public ArticuloView () : 
			base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult);
			saveAction.Activated += delegate {
				save ();
			};
		}
		//Si hay que actualizar el articulo
		public ArticuloView (object id) : this(){
			this.id = id;
			load ();
			//Versión anterior de update
			/*
			foreach (var fila in queryResultArt.Rows) {
				entryNombre.Text = fila [1].ToString ();
				QueryResult queryResultCat = PersisterHelper.Get ("select * from categoria");
				ComboBoxHelper.Fill (comboBoxCategoria, queryResultCat);
				comboBoxCategoria.Active = Int32.Parse(fila [2].ToString());
				spinButtonPrecio.Text =fila [3].ToString();
				spinButtonPrecio.Activate();

			}*/
			saveAction.Activated += delegate {
				updateArt (id);
			};
		}

		private void load(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id=@id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if(!dataReader.Read ()){
				//TODO throw exception
				return;
			}
			string nombre = (string)dataReader["nombre"];
			object categoria = dataReader["categoria"];
			decimal precio = (decimal)dataReader["precio"];
			dataReader.Close ();
			entryNombre.Text = nombre;
			comboBoxCategoria.Active = Convert.ToInt32(categoria);
			spinButtonPrecio.Value = Convert.ToDouble(precio);
		}

		private void save() {

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";
			Console.Write (entryNombre.Text);
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
			Console.Write (this.id);
			Console.Write (id);
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

