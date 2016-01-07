package org.institutoserpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Scanner;

import com.mysql.jdbc.Statement;

public class PruebaArticulo {

	private static Scanner tec;

	public static void main(String[] args) throws SQLException {
		
		tec = new Scanner(System.in);
		Connection connection=connectar();
		
		showMenu();
		selectMenu(tec.nextInt(),connection, tec);
		
		connection.close();
		System.out.println("fin");
}
	
	public static Connection connectar() throws SQLException{
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		return connection;
		
	}
	
	public static void doSelect(Connection connection) throws SQLException{
		String query = "SELECT * FROM articulo";
		//create the java statement
		Statement st = (Statement) connection.createStatement();
		// 	execute the query, and get a java resultset
		ResultSet rs = st.executeQuery(query);
		// iterate through the java resultset
		while (rs.next())
		{
			int id = rs.getInt("id");
		    String name = rs.getString("nombre");
		    Double precio = rs.getDouble("precio");
		     
		    // print the results
		    System.out.format("%s, %s, %s \n", id, name, precio);
		  }
	}

	public static void showMenu(){
		System.out.println(" 0.Salir\n "
				+ "1.Leer \n 2.Nuevo \n "
				+ "3.Editar \n 4.Eliminar\n "
				+ "5.Listar Todos.");
	}
	
	public static void selectOne(Scanner tec,Connection connection ) throws SQLException{
		System.out.println("Introduzca la id del articulo");
		int id=tec.nextInt();
		String query = "SELECT * FROM articulo where id='"+id+"'";
		//create the java statement
		Statement st = (Statement) connection.createStatement();
		// 	execute the query, and get a java resultset
		ResultSet rs = st.executeQuery(query);
		// iterate through the java resultset
		while (rs.next())
		{
		    String name = rs.getString("nombre");
		    Double precio = rs.getDouble("precio");	     
		    // print the results
		    System.out.format("%s, %s, %s \n", id, name, precio);
		 }
	}
	
	public static void newRegistry(Scanner tec,Connection connection ) throws SQLException{
		System.out.println("Introduzca el nombre del articulo");
		String nombre=tec.next();
		System.out.println("Introduzca el precio del articulo");
		float precio=tec.nextFloat();
		String query = "SELECT * FROM categoria";
		//create the java statement
		Statement st = (Statement) connection.createStatement();
		// 	execute the query, and get a java resultset
		ResultSet rs = st.executeQuery(query);
		// iterate through the java resultset
		while (rs.next())
		{
		    String name = rs.getString("nombre");
		    System.out.format("Categoria: "+ name+"\n");
		 }
		System.out.println("Selecione categoria");
		int categoria= tec.nextInt();
		try{
			String queryInser = "insert into articulo (id, nombre, precio, categoria) values (null, '"+nombre+"',' "+precio+"', '"+categoria+"')";
			//create the java statement
			Statement stNew = (Statement) connection.createStatement();
			// 	execute the query, and get a java resultset
			stNew.executeUpdate(queryInser);
			// iterate through the java resultset
		}catch(Exception sqlE){
			System.err.println(sqlE.getMessage());
		}
	}
	
	public static void selectMenu(int op, Connection connection, Scanner tec) throws SQLException{
		
		switch(op){
		case 0:break;
		case 1:selectOne(tec, connection);break;
		case 2:newRegistry(tec, connection);break;
		case 3:;break;
		case 4:;break;
		case 5:doSelect(connection);
		break;
		default: System.out.println("Elija una opción válida");;
		break;
		}
	}

}