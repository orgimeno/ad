using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace SerpisAd
{
	public class Persister
	{
		private const string INSERTSQL = "insert into {0} ({1}) values ({2})";
		public static int Insert(object obj){


			Type type = obj.GetType();

			string insertSQL = getInternalSQL (type);
			Console.WriteLine (insertSQL);
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = getInternalSQL (type);
			addParameters (dbCommand, obj);
			/*dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			DbCommandHelper.AddParameter (dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", articulo.Categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", articulo.Precio);*/
			return dbCommand.ExecuteNonQuery ();
		}

		public static string[] getFieldNames(Type type){
			List<string> fieldNames = new List<string> ();
			PropertyInfo[] propertyInfos = type.GetProperties();
			foreach(PropertyInfo propertyInfo in propertyInfos){
				if(!propertyInfo.Name.Equals("Id"))
					fieldNames.Add(propertyInfo.Name.ToLower());
			}
			return fieldNames.ToArray ();
		}
		private static string[] getParamNames(string[] fieldNames){
			List<string> paramNames = new List<string> ();
			foreach (string fieldName in fieldNames)
				paramNames.Add("@" + fieldName);
			return paramNames.ToArray();
		}

		private static string getInternalSQL(Type type){
			string tableName = type.Name.ToLower ();
			string[] fieldNames = getFieldNames (type);
			string[] paramNames = getParamNames (fieldNames);
			//dbCommand.CommandText="insert into "+type+" ("+string.Join(", ",fieldNames)+") values "+ string.Join(", ",paramNames);
			return string.Format(INSERTSQL, tableName, string.Join(", ",fieldNames),string.Join(", ",paramNames));
		}

		private static void addParameters(IDbCommand dbCommand, object obj){
			//string[] parameters = getFieldNames (obj.GetType ());
			PropertyInfo[] propertyInfos = obj.GetType ().GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos)
				if (!propertyInfo.Name.Equals ("Id")) {
					string name = propertyInfo.Name.ToLower ();
					object value = propertyInfo.GetValue (obj, null);
					DbCommandHelper.AddParameter (dbCommand, name, value);
				}
		}


	}

}

