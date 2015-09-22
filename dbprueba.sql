CREATE TABLE IF NOT EXISTS `articulo` (
  `id` bigint auto_increment primary key,
  `nombre` varchar(50) NOT NULL unique,
  `categoria` bigint(20) DEFAULT NULL,
  `precio` decimal(10,5) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `categoria` (
  `id` bigint auto_increment primary key,
  `nombre` varchar(50) NOT NULL UNIQUE
);