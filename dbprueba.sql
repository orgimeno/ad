CREATE TABLE IF NOT EXISTS `articulo` (
  `id` bigint auto_increment primary key,
  `nombre` varchar(50) NOT NULL unique,
  `categoria` bigint(20) DEFAULT NULL,
  `precio` decimal(10,5) DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS `categoria` (
  `id` bigint auto_increment primary key,
  `nombre` varchar(50) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS `cliente` (
  `id` bigint auto_increment primary key,
  `nombre` varchar(50) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS `pedido` (
  `id` bigint auto_increment primary key,
  `cliente` bigint not null,
  `fecha` datetime not null,
  `importe` decimal(10,2)
);

CREATE TABLE IF NOT EXISTS `pedidolinea` (
  `id` bigint auto_increment primary key,
  `pedido` bigint not null,
  `articulo` bigint not null,
  `precio` decimal(10,2),
  `unidades` decimal(10,2)
  `importe` decimal(10,2),
);