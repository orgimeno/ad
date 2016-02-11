package org.institutoserpis.ad;

import java.util.Calendar;
import java.util.Date;
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
	
	public static void show(Pedido pedido){
		System.out.println(pedido);
		for (PedidoLinea pedidoLinea : pedido.getPedidoLineas())
			System.out.println(pedidoLinea);
	}
	
	public static void query(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List <Pedido> pedidos = entityManager.createQuery("from Pedido", Pedido.class).getResultList();
		for (Pedido pedido : pedidos)
			show(pedido);
		entityManager.getTransaction().commit();
	}
	
	private static Long persist() {
		System.out.println("persist:");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Cliente cliente = entityManager.find(Cliente.class, 1L);
		Pedido pedido = new Pedido();
		pedido.setCliente(cliente);
		pedido.setFecha(Calendar.getInstance());
		
		//TODO lo que toque hacer
		entityManager.persist(pedido);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(pedido);
		return pedido.getId();
	}

}
