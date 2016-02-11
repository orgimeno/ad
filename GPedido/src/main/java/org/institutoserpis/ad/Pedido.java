package org.institutoserpis.ad;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;

import org.hibernate.annotations.GenericGenerator;

@Entity
public class Pedido { 

	private Long id;
	private Cliente cliente;
	private Calendar fecha;
	private List<PedidoLinea> pedidoLinea = new ArrayList<>();

	@Id
	@GeneratedValue(generator="increment")
	@GenericGenerator(name="increment", strategy = "increment")
	public Long getId() {
		return id;
	}
	public void setId(Long id) {
		this.id = id;
	}
	
	
	@ManyToOne
	@JoinColumn(name="cliente")
	
	public Cliente getCliente() {
		return cliente;
	}
	
	public void setCliente(Cliente cliente) {
		this.cliente = cliente;
	}
	
	public Calendar getFecha() {
		return fecha;
	}
	
	public void setFecha(Calendar fecha) {
		this.fecha = fecha;
	}
	
	public List<PedidoLinea> getPedidoLineas() {
		return pedidoLinea;
	}
	public void setPedidoLineas(List<PedidoLinea> pedidoLinea) {
		this.pedidoLinea = pedidoLinea;
	}
	public String toString(){
		return String.format("%s %s", id, cliente);
	}
}