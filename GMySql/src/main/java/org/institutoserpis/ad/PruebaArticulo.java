package org.institutoserpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.mysql.jdbc.Statement;

public class PruebaArticulo {

	public static void main(String[] args) throws SQLException {
		
		Connection connection=connectar();
		String query = "SELECT * FROM articulo";
		 
		  // create the java statement
		  Statement st = (Statement) connection.createStatement();
		   
		  // execute the query, and get a java resultset
		  ResultSet rs = st.executeQuery(query);
		   
		  // iterate through the java resultset
		  while (rs.next())
		  {
		    int id = rs.getInt("id");
		    String name = rs.getString("nombre");
		    Double precio = rs.getDouble("precio");
		    //String lastName = rs.getString("last_name");
		    //int numPoints = rs.getInt("num_points");
		     
		    // print the results
		    System.out.format("%s, %s, %s \n", id, name, precio);
		  }
		
		connection.close();
		System.out.println("fin");
}
	
	public static Connection connectar() throws SQLException{
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		return connection;
		
	}

}