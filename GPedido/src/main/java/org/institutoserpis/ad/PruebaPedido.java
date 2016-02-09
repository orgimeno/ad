package org.institutoserpis.ad;

import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;


public class PruebaPedido {
	
	private static EntityManagerFactory entityManagerFactory;

	public static void main(String[] args) {
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		System.out.println("inicio");
		entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		query();
		
		entityManagerFactory.close();
		System.out.print("fin");
	}
	
	public static void query(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List <Pedido> articulos = entityManager.createQuery("from Pedidos", Pedido.class).getResultList();
		for (Pedido articulo : articulos)
			System.out.println(articulo);
		entityManager.getTransaction().commit();
	}

}
