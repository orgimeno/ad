using System;
using System.Reflection;

namespace PReflection
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*object i=0;
			Type typeInt = i.GetType ();
			Console.WriteLine ("typeInt.Name={0}", typeInt.Name);

			object s="";
			Type typeS = s.GetType ();
			Console.WriteLine ("typeS.Name={0}",typeS.Name);

			Type typeX = typeof(String);
			showType (typeX);
			
			Type typeFoo = typeof(Foo);
			showType (typeFoo);
			
			Type typeArt = typeof(Articulo);
			showType (typeArt);

			Type typeO = typeof(object);
			showType (typeO);*/

			Articulo articulo = new Articulo ();

			articulo.Nombre="articulo1";
			articulo.Precio = decimal.Parse("2.6");
			articulo.Categoria = 2;

			setValues (articulo, new object[] { 33L, "nuevo 33 modificado", 3L, decimal.Parse("33.33") });
			showObject (articulo);

		}

		private static void showType(Type type){
			Console.WriteLine ("type.Name={0} type.FullName={1} type.BaseType={2}",
			                   type.Name, type.FullName, type.BaseType);	
			PropertyInfo[] propertyInfos = type.GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos){
				Console.WriteLine ("property.Name={0} property.PropertyInfo={1}",
				                   propertyInfo.Name, propertyInfo.PropertyType);	
			}
		}

		private static void showObject(object obj){
			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos = type.GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				Console.WriteLine("{0}={1}",propertyInfo.Name, propertyInfo.GetValue (obj,null));

			}
		}

		private static void setValues(object obj, object[] values){
			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos = type.GetProperties ();
			int index = 0;
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				propertyInfo.SetValue (obj, values [index++], null);
			}

		}
	}

	public class Foo{
		private string name;

		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
	}

	public class Articulo
	{
		public Articulo ()
		{
		}

		private object id;
		private string nombre;
		private object categoria;
		private decimal precio;

		public object Id {
			get { 
				return id;
			}
			set { 
				id = value;
			}
		}

		public string Nombre {
			get {
				return nombre;
			}
			set { 
				nombre = value; 
			}
		}


		public object Categoria {
			get {
				return categoria;
			}
			set {
				categoria = value;
			}
		}


		public decimal Precio {
			get {
				return precio;
			}
			set {
				precio = value;
			}
		}
	}
}
