use CecchettoLogica;
go

-- Datos de prueba para el dashboard.
-- Se puede correr varias veces: primero borra los servicios de prueba que haya.

begin transaction;

delete from detalles_servicio where id_servicio in
    (select id_servicio from servicios where descripcion like 'servicio demo%' or descripcion like '%(demo)');
delete from servicios where descripcion like 'servicio demo%' or descripcion like '%(demo)';

declare @id int;

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-01-04', 17, 3, 2, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 14);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-01-07', 12, 6, 3, 'Revision de bateria y alternador (demo)', 3, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-01-08', 17, 2, 1, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 1), (6, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-01-18', 13, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-02-10', 10, 2, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-02-24', 11, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-02-28', 4, 6, 3, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 9), (1, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-02-28', 18, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-03-09', 7, 9, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-03-14', 19, 1, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-03-28', 20, 15, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-03-30', 15, 8, 4, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-03-31', 7, 11, 2, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-04-09', 1, 6, 3, 'Recarga de bateria (demo)', 5, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 7);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-04-09', 13, 11, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-04-15', 10, 9, 2, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-04-18', 7, 10, 3, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-04-23', 16, 11, 2, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-04-29', 1, 8, 4, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-05-02', 7, 10, 3, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 11);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-05-05', 10, 10, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 12), (7, @id, 12);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-05-10', 5, 2, 1, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-05-13', 17, 13, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 1), (6, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-05-20', 3, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-05-21', 5, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-05-30', 8, 2, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-01', 20, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-04', 8, 11, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-09', 4, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-11', 6, 13, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-18', 2, 13, 4, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 14);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-19', 7, 14, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 2), (7, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-19', 19, 3, 2, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-20', 11, 6, 3, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 10);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-22', 9, 9, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-25', 8, 13, 4, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-06-26', 7, 1, 1, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 1), (7, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-03', 17, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 5), (4, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-05', 2, 8, 4, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-10', 10, 12, 3, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-13', 1, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-13', 5, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-16', 16, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-18', 2, 7, 4, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 15);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-18', 7, 13, 4, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-22', 9, 9, 2, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 2), (5, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-27', 1, 1, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-29', 1, 2, 1, 'Revision de bateria y alternador (demo)', 3, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-31', 7, 13, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 11);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-07-31', 10, 3, 2, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 7);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-03', 10, 2, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-05', 7, 7, 4, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-06', 6, 1, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 12);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-10', 9, 2, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-11', 8, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 12);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-11', 10, 9, 2, 'Mantenimiento preventivo de bateria (demo)', 2, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-12', 8, 13, 4, 'Recarga de bateria (demo)', 5, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 15);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-16', 2, 2, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-26', 6, 10, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-27', 2, 3, 2, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-28', 19, 14, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 14);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-08-30', 18, 8, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-09-11', 6, 1, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-09-11', 6, 2, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 10);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-09-13', 6, 1, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-09-17', 1, 1, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 12);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-09-18', 9, 12, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-09-22', 10, 6, 3, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-09-23', 7, 1, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-10-04', 11, 2, 1, 'Recarga de bateria (demo)', 5, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-10-11', 7, 10, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 7), (8, @id, 7);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-10-18', 6, 12, 3, 'Recarga de bateria (demo)', 5, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 12);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-10-19', 18, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 1), (3, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-10-26', 9, 6, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-10-26', 12, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-11-07', 8, 9, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-11-11', 12, 10, 3, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-11-17', 4, 14, 1, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 15), (9, @id, 15);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-11-22', 9, 14, 1, 'Revision de bateria y alternador (demo)', 3, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-11-25', 10, 14, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 11);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-11-28', 7, 8, 4, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-12-05', 6, 15, 2, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 4), (6, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-12-16', 2, 5, 3, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-12-20', 10, 9, 2, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-12-23', 2, 3, 2, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2025-12-26', 4, 10, 3, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 13);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-01-04', 1, 1, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 13);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-01-08', 7, 1, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-01-15', 19, 14, 1, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-01-16', 11, 2, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 1), (3, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-01-25', 3, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-02-02', 14, 2, 1, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 4), (8, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-02-09', 19, 14, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-02-13', 4, 10, 3, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 10);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-02-15', 14, 8, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 7);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-02-21', 7, 15, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-03-01', 19, 7, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 8), (3, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-03-10', 4, 7, 4, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-03-14', 7, 2, 1, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 2), (8, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-03-17', 2, 2, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-03-20', 2, 8, 4, 'Instalacion de bateria nueva (demo)', 1, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 1), (5, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-03-28', 10, 8, 4, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 5), (3, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-04-03', 4, 15, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-04-10', 1, 2, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-04-17', 7, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-04-17', 11, 1, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 13), (2, @id, 13);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-04-21', 20, 5, 3, 'Recarga de bateria (demo)', 5, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (1, @id, 14);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-04-26', 4, 2, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-04-29', 7, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 11);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-01', 6, 13, 4, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-03', 4, 12, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-15', 16, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-18', 6, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 2), (8, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-20', 20, 11, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-22', 6, 10, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-27', 9, 13, 4, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-30', 19, 12, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-05-31', 5, 8, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-04', 7, 1, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 7);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-04', 12, 10, 3, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 10);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-05', 1, 6, 3, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-08', 10, 14, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-08', 15, 1, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 7);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-08', 19, 2, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 11);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-09', 7, 2, 1, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-10', 12, 13, 4, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-13', 4, 1, 1, 'Cambio de bateria (demo)', 4, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-17', 8, 3, 2, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 15);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-18', 3, 9, 2, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 11), (10, @id, 11);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-06-24', 16, 5, 3, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-02', 10, 6, 3, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-04', 6, 10, 3, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-04', 18, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 13);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-07', 2, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-07', 5, 11, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-09', 1, 12, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-10', 11, 2, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-11', 7, 3, 2, 'Instalacion de bateria nueva (demo)', 1, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-13', 1, 8, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 2), (2, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-17', 5, 1, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-19', 4, 12, 3, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-22', 20, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-23', 2, 9, 2, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 13), (5, @id, 13);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-07-26', 1, 12, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-01', 16, 1, 1, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-02', 5, 2, 1, 'Recarga de bateria (demo)', 5, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-04', 16, 11, 2, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-06', 7, 10, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 14);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-15', 9, 6, 3, 'Revision de bateria y alternador (demo)', 3, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 11), (5, @id, 11);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-17', 17, 10, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 6);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-18', 2, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 8), (4, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-18', 4, 11, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-18', 5, 7, 4, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-18', 9, 1, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 9), (8, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-25', 6, 11, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (6, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-26', 16, 1, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-08-26', 19, 2, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-01', 8, 2, 1, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 7), (7, @id, 7);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-05', 6, 10, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 9);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-07', 5, 3, 2, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (3, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-09', 7, 9, 2, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (10, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-18', 11, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-22', 12, 3, 2, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (8, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-23', 3, 5, 3, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 5), (9, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-09-24', 6, 1, 1, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-10-09', 6, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 5);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-10-10', 6, 6, 3, 'Revision de bateria y alternador (demo)', 3, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 1);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-10-12', 13, 13, 4, 'Cambio de bateria (demo)', 4, 2);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 8);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-10-15', 7, 5, 3, 'Instalacion de bateria nueva (demo)', 1, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (2, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-10-23', 4, 5, 3, 'Recarga de bateria (demo)', 5, 4);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-10-23', 4, 6, 3, 'Cambio de bateria (demo)', 4, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (9, @id, 3);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-10-29', 5, 13, 4, 'Cambio de bateria (demo)', 4, 2);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 10);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-11-03', 1, 10, 3, 'Mantenimiento preventivo de bateria (demo)', 2, 3);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (7, @id, 4);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-11-10', 12, 10, 3, 'Revision de bateria y alternador (demo)', 3, 2);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (5, @id, 13);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-11-11', 3, 9, 2, 'Cambio de bateria (demo)', 4, 2);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 2);

insert into servicios (fecha_servicio, id_cliente, id_empleado, id_sucursal, descripcion, id_tipo_servicio, estado_servicio) values ('2026-11-18', 20, 12, 3, 'Cambio de bateria (demo)', 4, 2);
set @id = scope_identity();
insert into detalles_servicio (id_bateria, id_servicio, id_vehiculo) values (4, @id, 9);

commit transaction;
go
