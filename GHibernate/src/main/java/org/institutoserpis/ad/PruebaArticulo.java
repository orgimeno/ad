package org.institutoserpis.ad;

import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.util.List;
import java.util.Scanner;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

import com.sun.org.apache.xerces.internal.impl.xpath.regex.ParseException;

public class PruebaArticulo {
	private static Scanner scanner = new Scanner(System.in);
	
	public static void main(String[] args) {
		System.out.println("inicio");
		EntityManagerFactory entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();

		listarArticulos(entityManager);
		buscarArticulo(entityManager);
		eliminarArticulo(entityManager);
		entityManager.getTransaction().commit();
		entityManager.close();
		
		entityManagerFactory.close();
	}	
	
	private static BigDecimal scanBigDecimal(String label) throws java.text.ParseException {
		while (true) {
			System.out.print(label);
			String data = scanner.nextLine().trim();
			DecimalFormat decimalFormat = (DecimalFormat)DecimalFormat.getInstance();
			decimalFormat.setParseBigDecimal(true);
			try {
				return (BigDecimal)decimalFormat.parse(data);
			} catch (ParseException e) {
				System.out.println("Debe ser un número decimal");
			}
		}
	}
	
	private static Articulo scanArticulo() throws java.text.ParseException {
		Articulo articulo = new Articulo();
		articulo.setNombre(scanString(    "   Nombre: "));
		articulo.setCategoria(scanLong(      "Categoria: "));
		articulo.setPrecio(scanBigDecimal("   Precio: "));
		return articulo;
	}
	
	private static long scanLong(String label) {
		while (true) {
			System.out.print(label);
			String data = scanner.nextLine().trim();
			try {
				return Long.parseLong(data);
			} catch (NumberFormatException ex) {
				System.out.println("Debe ser un número");
			}
		}
	}	
	private static String scanString(String label) {
		System.out.print(label);
		return scanner.nextLine().trim();
	}
	
	private static void showData(Articulo articulo){
		System.out.printf("%5s %-30s %5s %10s\n", 
				articulo.getId(), 
				articulo.getNombre(), 
				articulo.getCategoria(), 
				articulo.getPrecio()
		);
	}
	
	public static void listarArticulos(EntityManager entityManager){
		List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for (Articulo articulo : articulos)
			showData(articulo);
	}
	
	public static void buscarArticulo(EntityManager entityManager){
		long id = scanLong("Introduzca una id para buscar");
		Articulo articulo = entityManager.find(Articulo.class, id);
		showData(articulo);

	}
	
	public static void eliminarArticulo(EntityManager entityManager){
		long id = scanLong("Introduzca una id para eliminar");
		Articulo articulo = entityManager.find(Articulo.class, id);
		entityManager.remove(articulo);
	}
	
	public static void nuevoArticulo(EntityManager entityManager){
		
	}

}
