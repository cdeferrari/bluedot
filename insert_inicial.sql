
insert into direccion  (street ,number ,latitud ,longitud)  values ('lynch', '450', null,null);
insert into administracion (name  ,cuit  ,direction_id  ,start_date)  values ('administracion bluedot','201112223331',1, Now());

insert into consorcio (friendly_name ,cuit ,mailing_list ,administration_id) VALUES (
   'bluedot friendly'  -- friendly_name - IN varchar(100)
  ,'201112223332'  -- cuit - IN varchar(50)
  ,'micorreo@gmail.com'  -- mailing_list - IN varchar(50)
  ,1   -- administration_id - IN int(11)
);


insert into estado_ticket (description) VALUES ('open');
insert into estado_ticket (description) VALUES ('closed');
insert into propiedad (direction_id) values (1);
insert into datos_contacto (telephone ,cellphone ,email) VALUES (
   '4444444'  -- telephone - IN varchar(100)
  ,'1134532407'  -- cellphone - IN varchar(100)
  ,'leandro.anacondio@gmail.com'  -- email - IN varchar(100)
);

insert into prioridad (value ,description) VALUES (1,'bloqueante');

insert into usuario (dni ,cuit ,name ,surname ,data_contact_id) VALUES (
   '32991942'  -- dni - IN varchar(50)
  ,'20329919421'  -- cuit - IN varchar(100)
  ,'leandro'  -- name - IN varchar(100)
  ,'anacondio'  -- surname - IN varchar(100)
  ,1   -- data_contact_id - IN int(11)
);

insert into propietario (user_id) VALUES (1);
insert into tipo_pago (description) VALUES ('tarjeta de credito');
insert into inquilino (user_id ,payment_type_id) VALUES (1,1);
insert into empleado (user_id ,administration_id) VALUES (1,1);

insert into unidad (property_id ,piso ,dto ,owner_id ,renter_id) VALUES (1,1,1,1,1);
insert into rol (description
) VALUES ('admin'  -- description - IN varchar(50)
);
insert into usuario_backlog (user_id, role_id, password ) values (1, 1, '123456');

