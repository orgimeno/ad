using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private Articulo articulo;

		//Si es un articulo nuevo
		public ArticuloView () : 
			base(Gtk.WindowType.Toplevel)
		{
			init ();
			saveAction.Activated += delegate {	save();	};
		}
		//Si hay que actualizar el articulo
		public ArticuloView (object id) : base(WindowType.Toplevel) {
			articulo.Id = id;
			load ();
			init ();
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
			saveAction.Activated += delegate {updateArt (id);};
		}

		private void init() {
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, articulo.Categoria);
			spinButtonPrecio.Value = Convert.ToDouble(articulo.Precio);

		}

		private void load() {
			ArticuloPersister.Load (articulo.Id);
		}

		private void save() {
			Articulo articulo = new Articulo ();
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);
			ArticuloPersister.Insert (articulo);
			Destroy ();
		}

		private void updateArt(object id){
			Articulo articulo = new Articulo ();
			articulo.Id = id;
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);
			ArticuloPersister.Update (articulo);
			Destroy ();

		}

	}
}

