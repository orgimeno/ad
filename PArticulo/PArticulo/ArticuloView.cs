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
			articulo = new Articulo ();
			articulo.Nombre = "";
			init ();
			saveAction.Activated += delegate {	save();	};
		}
		//Si hay que actualizar el articulo
		public ArticuloView (object id) : base(WindowType.Toplevel) {
			articulo = ArticuloPersister.Load(id);
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

		private void updateModel(){
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			articulo.Precio = Convert.ToDecimal(spinButtonPrecio.Value);
		}

		private void save() {
			updateModel ();
			//ArticuloPersister.Insert (articulo);
			Persister.Insert (articulo);
			Destroy ();
		}

		private void updateArt(object id){
			articulo.Id = id;
			updateModel ();
			ArticuloPersister.Update (articulo);
			Destroy ();

		}

	}
}

