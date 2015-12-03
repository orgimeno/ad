using System;
using System.Reflection;

namespace SerpisAd
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//			object i=33;
			//			Type typeI = i.GetType ();
			//			showType (typeI);
			//
			//			object s="Hola";
			//			Type typeS = s.GetType (); 
			//			showType (typeS);
			//
			//			Type typeX = typeof(string);
			//			showType (typeX);
			//
			//
			//			Type typeO = typeof(object);
			//			showType (typeO);

			//			Type typeFoo = typeof(Foo);
			//			showType (typeFoo);
			Articulo articulo = new Articulo ();
			//showType (articulo.GetType());
			//			articulo.Nombre = "nuevo 33";
			//			articulo.Categoria = 2;
			//			articulo.Precio = decimal.Parse ("3,5");;
			setValues (articulo, 
			           new object[] { 33L, "nuevo 33 modificado", 3L, decimal.Parse("33,33") });
			showObject (articulo);
		}

		private static void showType(Type type) {
			Console.WriteLine ("type.Name={0} type.FullName={1} type.BaseType.Name={2}", 
			                   type.Name, type.FullName, type.BaseType.Name);
			PropertyInfo[] propertyInfos = type.GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos)
				Console.WriteLine ("propertyInfo.Name={0} propertyInfo.PropertyType={1}", 
				                   propertyInfo.Name, propertyInfo.PropertyType);
		}

		private static void showObject(object obj) {
			Type type = obj.GetType ();
			if (!(obj is Attribute)) {
				object[] attributes = type.GetCustomAttributes (true);
				foreach (Attribute attribute in attributes)
					showObject (attribute);
			}
			PropertyInfo[] propertyInfos = type.GetProperties ();
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				if (propertyInfo.IsDefined (typeof(IdAttribute), true)) 
					Console.WriteLine ("{0} decorado con IdAttribute", propertyInfo.Name);
				Console.WriteLine ("{0}={1}", 
				                   propertyInfo.Name, propertyInfo.GetValue (obj, null));
			}
		}

		private static void setValues(object obj, object[] values) {
			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos = type.GetProperties ();
			int index = 0;
			foreach (PropertyInfo propertyInfo in propertyInfos) 
				propertyInfo.SetValue (obj, values [index++], null);

		}
	}

	public class IdAttribute : Attribute {
	}

	public class TableAttribute : Attribute {
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

	public class Foo {
		private object id;

		public object Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}

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

	[Table(Name = "article")]
	public class Articulo
	{
		public Articulo ()
		{
		}

		private object id;
		private string nombre;
		private object categoria;
		private decimal precio;

		[Id]
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
