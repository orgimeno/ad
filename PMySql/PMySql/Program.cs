using System;
using MySql.Data.MySqlClient;

namespace PMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MySqlConnection mysqlconnection = new MySqlConnection (
				"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas");
			mysqlconnection.Open ();
			MySqlCommand mysqlcommand = mysqlconnection.CreateCommand ();//Devuelve objeto del tipo MySqlCommand
			mysqlcommand.CommandText = "select * from articulo";
			MySqlDataReader msqldr = mysqlcommand.ExecuteReader ();// Devuelve un objeto del tipo MySqlDataReader

			/*
			while (msqldr.Read()) {
				Console.WriteLine ("id={0} nombre={1}", msqldr["id"], msqldr["nombre"]);
			}
			*/

			show (msqldr,showColumnNames (msqldr));
			msqldr.Close();
			mysqlconnection.Close ();
		}

		private static string[] showColumnNames(MySqlDataReader mysqldatareader){
			int columns = mysqldatareader.FieldCount;
			string[] columnasNombre = new string[columns];
			for (int i=columns-1; i>=0; i--)
				columnasNombre[i] = mysqldatareader.GetName (i);
			return columnasNombre;
		
		}

		private static void show(MySqlDataReader msdr, string[] columnasNombre){
			while (msdr.Read()) {
				//for (int i=0; i<columnasNombre.Length; i++)
				foreach (string nombre in columnasNombre)
					Console.WriteLine (nombre +": "+msdr[nombre]);
					//Console.WriteLine (columnasNombre[i]+": "+msdr[columnasNombre[i]]);

			}
		}
	}	
}