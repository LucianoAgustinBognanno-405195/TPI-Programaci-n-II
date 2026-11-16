-- 1. tipos de vehiculos
insert into tipos_vehiculos (vehiculo) values
('automovil'),
('motocicleta'),
('camioneta'),
('camion');

-- 2. tipos de baterias
insert into tipos_baterias (tipos) values
('plomo-acido'),
('agm'),
('gel'),
('litio'),
('plomo-calcio');

-- 3. ciudades
insert into ciudades (ciudad) values
('cordoba'),
('villa carlos paz'),
('rio cuarto'),
('villa maria'),
('san francisco'),
('alta gracia'),
('jesus maria'),
('bell ville'),
('marcos juarez'),
('mataco'),
('monte maiz');

-- 4. tipos de contactos
insert into tipos_contactos (tipo_contacto) values
('telefono'),
('correo electronico');

-- 5. tamanios de bateria
insert into tamanios_bateria (dimensiones, grupo) values
('293 x 175 x 175 mm', 'grupo 21'),
('237 x 173 x 173 mm', 'grupo 24'),
('242 x 175 x 190 mm', 'grupo 27'),
('278 x 175 x 190 mm', 'grupo 31'),
('354 x 175 x 175 mm', 'grupo 8D');

-- 6. tipos de empleados
insert into tipos_empleados (tipo_empleado) values
('administrativo'),
('vendedor'),
('tecnico'),
('gerente'),
('supervisor'),
('repositor'),
('encargado de deposito'),
('atencion al cliente');

-- 7. tipos de facturas
insert into tipos_facturas (tipo_factura) values
('A'),
('B'),
('C'),
('nota de debito'),
('nota de credito'),
('factura de compra');

-- 8. voltajes
insert into voltajes (voltaje) values
('6V'),
('12V'),
('24V'),
('48V');

-- 9. marcas
insert into marcas (descripcion) values
('moura'),
('bosch'),
('willard'),
('varta'),
('heliar');

-- 10. tipos de clientes
insert into tipos_clientes (tipo) values
('particular'),
('mayorista'),
('minorista');

-- 11. estados de servicio
insert into estados_servicio (estado) values
('pendiente'),     -- 1
('en proceso'),    -- 2
('finalizado'),    -- 3
('cancelado');     -- 4

-- 12. cuotas
insert into cuotas (cuota, recargo) values
(1, 0),     -- 1
(3, 10),    -- 2
(6, 15),    -- 3
(9, 20),    -- 4
(12, 30);   -- 5

-- 13. tipos de servicios
insert into tipos_servicios (descripcion) values
('instalacion'),        -- 1
('mantenimiento'),      -- 2
('revision'),           -- 3
('cambio de bateria'),  -- 4
('recarga');            -- 5

-- 14. tipos de documentos
insert into tipos_documentos (tipos) values
('dni'),
('cuit');

-- 15. movimientos
insert into movimientos (movimiento) values
('ingreso por compra'),
('egreso por venta'),
('devolucion de cliente'),
('devolucion a proveedor'),
('transferencia de sucursal'),
('baja por defecto'),
('ingreso por canje'),
('salida para garantia');

-- 16. medios de pagos  -- CORREGIDO: sin repetidos
insert into medios_pagos (medio) values
('efectivo'),                -- 1
('tarjeta de debito'),       -- 2
('tarjeta de credito'),      -- 3
('transferencia bancaria'),  -- 4
('mercado pago'),            -- 5
('cheque'),                  -- 6
('cuenta corriente'),        -- 7
('deposito bancario');       -- 8

-- 17. barrios (5 por ciudad, 55 en total)
insert into barrios (barrio, id_ciudad) values
-- cordoba (id 1) -> barrios 1-5
('nueva cordoba', 1),
('general paz', 1),
('alta cordoba', 1),
('centro', 1),
('jardin', 1),
-- villa carlos paz (id 2) -> barrios 6-10
('centro', 2),
('villa dominguez', 2),
('sol y lago', 2),
('la quinta', 2),
('miguel munoz', 2),
-- rio cuarto (id 3) -> barrios 11-15
('centro', 3),
('banda norte', 3),
('alberdi', 3),
('las ferias', 3),
('lomitas de oro', 3),
-- villa maria (id 4) -> barrios 16-20
('centro', 4),
('roque saenz pena', 4),
('barrancas del rio', 4),
('lamadrid', 4),
('san martin', 4),
-- san francisco (id 5)
('centro', 5),
('roque saenz pena', 5),
('la florida', 5),
('jardines del sur', 5),
('san cayetano', 5),
-- alta gracia (id 6)
('centro', 6),
('sur', 6),
('norte', 6),
('liniers', 6),
('pellegrini', 6),
-- jesus maria (id 7)
('centro', 7),
('guaico', 7),
('la costanera', 7),
('sierras y parques', 7),
('san jose', 7),
-- bell ville (id 8)
('centro', 8),
('sur', 8),
('norte', 8),
('este', 8),
('oeste', 8),
-- marcos juarez (id 9)
('centro', 9),
('villa argentina', 9),
('sur', 9),
('norte', 9),
('este', 9),
-- mataco (id 10)
('centro', 10),
('san martin', 10),
('belgrano', 10),
('sur', 10),
('norte', 10),
-- monte maiz (id 11)
('centro', 11),
('bobato', 11),
('evita', 11),
('santa barbara', 11),
('casitas', 11);

-- 18. direcciones (una por barrio: direccion N = barrio N)
insert into direcciones (calle, altura, id_barrio) values
('san martin', 125, 1),
('colon', 132, 2),
('rivadavia', 139, 3),
('belgrano', 146, 4),
('sarmiento', 153, 5),
('mitre', 160, 6),
('alvear', 167, 7),
('libertad', 174, 8),
('buenos aires', 181, 9),
('moreno', 188, 10),
('san martin', 195, 11),
('colon', 202, 12),
('rivadavia', 209, 13),
('belgrano', 216, 14),
('sarmiento', 223, 15),
('mitre', 230, 16),
('alvear', 237, 17),
('libertad', 244, 18),
('buenos aires', 251, 19),
('moreno', 258, 20),
('san martin', 265, 21),
('colon', 272, 22),
('rivadavia', 279, 23),
('belgrano', 286, 24),
('sarmiento', 293, 25),
('mitre', 300, 26),
('alvear', 307, 27),
('libertad', 314, 28),
('buenos aires', 321, 29),
('moreno', 328, 30),
('san martin', 335, 31),
('colon', 342, 32),
('rivadavia', 349, 33),
('belgrano', 356, 34),
('sarmiento', 363, 35),
('mitre', 370, 36),
('alvear', 377, 37),
('libertad', 384, 38),
('buenos aires', 391, 39),
('moreno', 398, 40),
('san martin', 405, 41),
('colon', 412, 42),
('rivadavia', 419, 43),
('belgrano', 426, 44),
('sarmiento', 433, 45),
('mitre', 440, 46),
('alvear', 447, 47),
('libertad', 454, 48),
('buenos aires', 461, 49),
('moreno', 468, 50),
('san martin', 475, 51),
('colon', 482, 52),
('rivadavia', 489, 53),
('belgrano', 496, 54),
('sarmiento', 503, 55);

-- 19. vehiculos
-- 1-4 autos, 5-7 motos, 8-10 camionetas, 11-15 camiones
insert into vehiculos (descripcion, id_tipo_vehiculo) values
('fiat cronos', 1),
('toyota corolla', 1),
('chevrolet onix', 1),
('ford ka', 1),
('honda wave', 2),
('yamaha fz', 2),
('suzuki gn 125', 2),
('ford ranger', 3),
('toyota hilux', 3),
('volkswagen amarok', 3),
('mercedes benz actros', 4),
('scania r450', 4),
('volvo fh', 4),
('iveco stralis', 4),
('ford cargo', 4);

-- 20. sucursales  -- CORREGIDO: direccion dentro de su ciudad
insert into sucursales (sucursal, id_direccion, id_vehiculo) values
('sucursal centro cba', 4, 1),      -- belgrano 146, centro (cordoba)
('sucursal alta cba', 3, 3),        -- rivadavia 139, alta cordoba
('sucursal rio cuarto', 11, 7),     -- san martin 195, centro (rio cuarto)
('sucursal villa maria', 16, 10);   -- mitre 230, centro (villa maria)

-- 21. vehiculos_sucursales
insert into vehiculos_sucursales (id_vehiculo, id_sucursal) values
(1, 1), (2, 1), (5, 1), (11, 1), (12, 1),
(3, 2), (4, 2), (6, 2), (13, 2),
(7, 3), (8, 3), (9, 3), (14, 3),
(10, 4), (15, 4), (1, 4), (3, 4);

-- 22. documentos (1-20 clientes, 21-35 empleados)
insert into documentos (id_tipo_documento, documento) values
(1, 30123456), (1, 30234567), (1, 30345678), (1, 30456789), (1, 30567900),
(1, 30679011), (1, 30790122), (1, 30901233), (1, 31012344), (1, 31123455),
(1, 31234566), (1, 31345677), (1, 31456788), (1, 31567899), (1, 31679010),
(1, 31790121), (1, 31901232), (1, 32012343), (1, 32123454), (1, 32234565),
(1, 32345676), (1, 32456787), (1, 32567898), (1, 32679009), (1, 32790120),
(1, 32901231), (1, 33012342), (1, 33123453), (1, 33234564), (1, 33345675),
(1, 33456786), (1, 33567897), (1, 33679008), (1, 33790119), (1, 33901230);

-- 23. clientes
insert into clientes (nombre, apellido, id_documento, id_direccion, id_tipo_cliente) values
('juan', 'perez', 1, 21, 1),
('maria', 'gomez', 2, 22, 1),
('carlos', 'rodriguez', 3, 23, 1),
('laura', 'fernandez', 4, 24, 1),
('martin', 'lopez', 5, 25, 1),
('transportes del centro', 'sa', 6, 26, 2),
('distribuidora norte', 'srl', 7, 27, 2),
('lucas', 'martinez', 8, 28, 3),
('ana', 'diaz', 9, 29, 1),
('taller el motor', 'srl', 10, 30, 2),
('nicolas', 'acosta', 11, 41, 1),
('valeria', 'silva', 12, 42, 1),
('gabriel', 'ortiz', 13, 43, 2),
('romina', 'navarro', 14, 44, 1),
('esteban', 'molina', 15, 45, 3),
('paula', 'gimenez', 16, 46, 1),
('federico', 'arias', 17, 47, 2),
('carolina', 'pereyra', 18, 48, 1),
('leandro', 'cabrera', 19, 49, 3),
('daniela', 'vazquez', 20, 50, 1);

-- 24. empleados
-- sucursal 1: empleados 1, 2, 14 | sucursal 2: 3, 4, 9, 11, 15
-- sucursal 3: 5, 6, 10, 12      | sucursal 4: 7, 8, 13
insert into empleados
(nombre, apellido, id_documento, id_direccion, id_sucursal, id_tipo_empleado, usuario, contrasenia, activo)
values
('pedro', 'sanchez', 21, 31, 1, 1, 'usuario1', 'clave1', 1),
('sofia', 'romero', 22, 32, 1, 2, 'usuario2', 'clave2', 1),
('diego', 'torres', 23, 33, 2, 3, 'usuario3', 'clave3', 1),
('valentina', 'ruiz', 24, 34, 2, 4, 'usuario4', 'clave4', 0),   -- CORREGIDO: inactiva para probar baja logica
('facundo', 'castro', 25, 35, 3, 5, 'usuario5', 'clave5', 1),
('camila', 'herrera', 26, 36, 3, 6, 'usuario6', 'clave6', 1),
('matias', 'alvarez', 27, 37, 4, 7, 'usuario7', 'clave7', 1),
('julieta', 'benitez', 28, 38, 4, 8, 'usuario8', 'clave8', 1),
('agustin', 'medina', 29, 39, 2, 2, 'usuario9', 'clave9', 1),
('florencia', 'suarez', 30, 40, 3, 3, 'usuario10', 'clave10', 1),
('mariano', 'dominguez', 31, 41, 2, 2, 'usuario11', 'clave11', 1),
('micaela', 'ibanez', 32, 42, 3, 3, 'usuario12', 'clave12', 1),
('roberto', 'sosa', 33, 43, 4, 4, 'usuario13', 'clave13', 1),
('agustina', 'acuna', 34, 44, 1, 5, 'usuario14', 'clave14', 1),     -- CORREGIDO: sucursal 5 -> 1
('sebastian', 'caceres', 35, 45, 2, 6, 'usuario15', 'clave15', 1);  -- CORREGIDO: sucursal 6 -> 2

-- 25. contactos
insert into contactos (id_tipo_contacto, id_cliente, id_empleado) values
(1, 1, null), (2, 2, null), (1, 3, null), (2, 4, null), (1, 5, null),
(2, 6, null), (1, 7, null), (2, 8, null), (1, 9, null), (2, 10, null),
(1, 11, null), (2, 12, null), (1, 13, null), (2, 14, null), (1, 15, null),
(2, null, 1), (1, null, 2), (2, null, 3), (1, null, 4), (2, null, 5),
(1, null, 6), (2, null, 7), (1, null, 8), (2, null, 9), (1, null, 10),
(2, null, 11), (1, null, 12), (2, null, 13), (1, null, 14), (2, null, 15);

-- 26. servicios  -- CORREGIDO: fechas en 2026 y sucursal = sucursal del empleado
-- (cliente, empleado, descripcion, tipo y estado son los originales)
insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values
('2026-01-12', 1, 1, 1, 'servicio de bateria 1', 1, 1),
('2026-01-28', 2, 2, 1, 'servicio de bateria 2', 2, 2),
('2026-02-15', 3, 3, 2, 'servicio de bateria 3', 3, 3),
('2026-03-04', 4, 4, 2, 'servicio de bateria 4', 4, 4),
('2026-03-25', 5, 5, 3, 'servicio de bateria 5', 5, 1),
('2026-04-17', 6, 6, 3, 'servicio de bateria 6', 1, 2),
('2026-05-09', 7, 7, 4, 'servicio de bateria 7', 2, 3),
('2026-06-02', 8, 8, 4, 'servicio de bateria 8', 3, 4),
('2026-06-27', 9, 9, 2, 'servicio de bateria 9', 4, 1),
('2026-07-21', 10, 10, 3, 'servicio de bateria 10', 5, 2),
('2026-08-14', 11, 11, 2, 'servicio de bateria 11', 1, 3),
('2026-09-08', 12, 12, 3, 'servicio de bateria 12', 2, 4),
('2026-10-01', 13, 13, 4, 'servicio de bateria 13', 3, 1),
('2026-10-23', 14, 14, 1, 'servicio de bateria 14', 4, 2),
('2026-11-06', 15, 15, 2, 'servicio de bateria 15', 5, 3);

-- 27. baterias
insert into baterias (id_tipo_bateria, id_voltaje, id_marca, id_tamanio_bateria) values
(1, 2, 1, 1),
(2, 2, 2, 2),
(3, 2, 3, 3),
(4, 3, 4, 4),
(5, 2, 5, 5),
(1, 2, 1, 1),
(2, 3, 2, 2),
(3, 2, 3, 3),
(4, 3, 4, 4),
(5, 2, 5, 5);

-- 28. stocks
insert into stocks (id_bateria, id_movimiento, id_sucursal, cantidad) values
(1, 1, 1, 25),
(2, 1, 2, 18),
(3, 2, 3, 12),
(4, 1, 4, 20),
(5, 3, 1, 8),
(6, 5, 2, 15),
(7, 2, 3, 10),
(8, 6, 4, 6),
(9, 7, 1, 14),
(10, 8, 2, 9);

-- 29. detalles_servicio  -- CORREGIDO: los 55 originales menos (1,1,1) que estaba repetido
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values
(1, 1, 1),
(6, 1, 1),
(6, 1, 9),
(2, 2, 2),
(7, 2, 2),
(2, 2, 1),
(7, 2, 9),
(3, 3, 3),
(8, 3, 3),
(3, 3, 1),
(8, 3, 10),
(4, 4, 4),
(9, 4, 4),
(4, 4, 1),
(9, 4, 11),
(5, 5, 5),
(10, 5, 5),
(5, 5, 1),
(10, 5, 11),
(6, 6, 6),
(1, 6, 6),
(6, 6, 2),
(1, 6, 11),
(7, 7, 7),
(2, 7, 7),
(7, 7, 2),
(2, 7, 11),
(8, 8, 8),
(3, 8, 8),
(8, 8, 3),
(3, 8, 12),
(9, 9, 9),
(4, 9, 9),
(9, 9, 6),
(4, 9, 14),
(10, 10, 10),
(5, 10, 10),
(10, 10, 6),
(5, 10, 14),
(1, 11, 11),
(6, 11, 11),
(1, 11, 7),
(2, 12, 12),
(7, 12, 12),
(2, 12, 5),
(3, 13, 13),
(8, 13, 13),
(3, 13, 5),
(4, 14, 14),
(9, 14, 14),
(4, 14, 5),
(5, 15, 15),
(10, 15, 15),
(5, 15, 8);

-- 30. facturas  -- CORREGIDO: cliente y fecha = los del servicio que factura
-- (los tipos de factura son los originales)
insert into facturas (id_cliente, id_tipo_factura, fecha) values
(1, 1, '2026-01-12'),   -- factura 1 -> servicio 1
(2, 2, '2026-01-28'),   -- factura 2 -> servicio 2
(3, 3, '2026-02-15'),   -- factura 3 -> servicio 3
(4, 1, '2026-03-04'),   -- factura 4 -> servicio 4
(5, 2, '2026-03-25'),   -- factura 5 -> servicio 5
(6, 3, '2026-04-17'),   -- factura 6 -> servicio 6
(7, 1, '2026-05-09'),   -- factura 7 -> servicio 7
(8, 2, '2026-06-02'),   -- factura 8 -> servicio 8
(9, 3, '2026-06-27'),   -- factura 9 -> servicio 9
(10, 1, '2026-07-21'),   -- factura 10 -> servicio 10
(11, 2, '2026-08-14'),   -- factura 11 -> servicio 11
(12, 3, '2026-09-08'),   -- factura 12 -> servicio 12
(13, 1, '2026-10-01'),   -- factura 13 -> servicio 13
(14, 2, '2026-10-23'),   -- factura 14 -> servicio 14
(15, 3, '2026-11-06'),   -- factura 15 -> servicio 15
(1, 1, '2026-01-12'),   -- factura 16 -> servicio 1
(2, 2, '2026-01-28'),   -- factura 17 -> servicio 2
(3, 3, '2026-02-15'),   -- factura 18 -> servicio 3
(4, 1, '2026-03-04'),   -- factura 19 -> servicio 4
(5, 2, '2026-03-25'),   -- factura 20 -> servicio 5
(6, 3, '2026-04-17'),   -- factura 21 -> servicio 6
(7, 1, '2026-05-09'),   -- factura 22 -> servicio 7
(8, 2, '2026-06-02'),   -- factura 23 -> servicio 8
(9, 3, '2026-06-27'),   -- factura 24 -> servicio 9
(10, 1, '2026-07-21'),   -- factura 25 -> servicio 10
(11, 2, '2026-08-14'),   -- factura 26 -> servicio 11
(12, 3, '2026-09-08'),   -- factura 27 -> servicio 12
(13, 1, '2026-10-01'),   -- factura 28 -> servicio 13
(14, 2, '2026-10-23'),   -- factura 29 -> servicio 14
(15, 3, '2026-11-06');  -- factura 30 -> servicio 15

-- 31. detalles_facturas (originales, sin cambios)
insert into detalles_facturas (id_servicio, id_factura, pre_unitario, cantidad, subtotal) values
(1, 1, 85000, 1, 85000),
(2, 2, 89500, 1, 89500),
(3, 3, 94000, 2, 188000),
(4, 4, 98500, 1, 98500),
(5, 5, 103000, 1, 103000),
(6, 6, 107500, 2, 215000),
(7, 7, 112000, 1, 112000),
(8, 8, 116500, 1, 116500),
(9, 9, 121000, 2, 242000),
(10, 10, 125500, 1, 125500),
(11, 11, 130000, 1, 130000),
(12, 12, 134500, 2, 269000),
(13, 13, 139000, 1, 139000),
(14, 14, 143500, 1, 143500),
(15, 15, 148000, 2, 296000),
(1, 16, 152500, 1, 152500),
(2, 17, 157000, 1, 157000),
(3, 18, 161500, 2, 323000),
(4, 19, 166000, 1, 166000),
(5, 20, 170500, 1, 170500),
(6, 21, 175000, 2, 350000),
(7, 22, 179500, 1, 179500),
(8, 23, 184000, 1, 184000),
(9, 24, 188500, 2, 377000),
(10, 25, 193000, 1, 193000),
(11, 26, 197500, 1, 197500),
(12, 27, 202000, 2, 404000),
(13, 28, 206500, 1, 206500),
(14, 29, 211000, 1, 211000),
(15, 30, 215500, 2, 431000);

-- 32. facturas_x_medios  -- CORREGIDO: importe = subtotal + recargo; medios 9 y 10 (repetidos) -> 4 y 7
insert into facturas_x_medios (id_medio_pago, id_cuota, id_detalle_factura, importe) values
(1, 1, 1, 85000.00),
(2, 1, 2, 89500.00),
(3, 3, 3, 216200.00),
(4, 1, 4, 98500.00),
(5, 3, 5, 118450.00),
(6, 1, 6, 215000.00),
(7, 2, 7, 123200.00),
(8, 3, 8, 133975.00),
(4, 1, 9, 242000.00),
(7, 5, 10, 163150.00);
go


-- control rapido
select s.id_servicio, s.fecha_servicio, c.nombre + ' ' + c.apellido as cliente,
       e.nombre + ' ' + e.apellido as empleado, su.sucursal, es.estado, ts.descripcion as tipo
from servicios s
join clientes c on c.id_cliente = s.id_cliente
join empleados e on e.id_empleado = s.id_empleado
join sucursales su on su.id_sucursal = s.id_sucursal
join estados_servicio es on es.id_estado = s.estado_servicio
join tipos_servicios ts on ts.id_tipo_servicio = s.id_tipo_servicio
order by s.fecha_servicio;