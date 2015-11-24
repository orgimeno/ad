using System;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public class ArticuloPersister
	{
		public static Articulo Load(object id){

			Articulo articulo = new Articulo ();
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ()) {
				//TODO throw exception
				throw new NotImplementedException();
				return articulo;
			}
			articulo.Nombre = (string)dataReader ["nombre"];
			articulo.Categoria = dataReader ["categoria"];
			if (articulo.Categoria is DBNull)
				articulo.Categoria = null;
			articulo.Precio = (decimal)dataReader ["precio"];
			dataReader.Close ();
			return articulo;

		}

		public static void Insert(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";
			string nombre = articulo.Nombre;
			object categoria = articulo.Categoria;
			decimal precio = articulo.Precio;

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
		}

		public static void Update(Articulo articulo){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre=@nombre, categoria=@categoria, precio=@precio"+
				" where id=@id"; 

			object id = articulo.Id;
			string nombre = articulo.Nombre;
			object categoria = articulo.Categoria;
			decimal precio = articulo.Precio;

			DbCommandHelper.AddParameter (dbCommand, "id", id);
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
		}
	}
}

