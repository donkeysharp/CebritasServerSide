-- Parent Categories
insert into Category(Name, SpanishName, ParentId)
values('Restaurants', 'Restaurantes', null)

insert into Category(Name, SpanishName, ParentId)
values('Bars', 'Bares', null)

insert into Category(Name, SpanishName, ParentId)
values('Night Clubs', 'Discotecas', null)

insert into Category(Name, SpanishName, ParentId)
values('Movies', 'Cines', null)

insert into Category(Name, SpanishName, ParentId)
values('Sports', 'Deportes', null)

insert into Category(Name, SpanishName, ParentId)
values('Hotels', 'Hoteles', null)

insert into Category(Name, SpanishName, ParentId)
values('Hospitals', 'Hospitales', null)

insert into Category(Name, SpanishName, ParentId)
values('Banks', 'Bancos', null)

insert into Category(Name, SpanishName, ParentId)
values('Bus Terminals', 'Terminal de Buses', null)

insert into Category(Name, SpanishName, ParentId)
values('Art & Culture', 'Arte y Cultura', null)

*/

/* Children */
insert into Category(Name, SpanishName, ParentId)
values('Fast Food', 'Comida Rápida', 1)

insert into Category(Name, SpanishName, ParentId)
values('International', 'Internacional', 1)

insert into Category(Name, SpanishName, ParentId)
values('Local', 'Nacional', 1)

insert into Category(Name, SpanishName, ParentId)
values('Street Food', 'Comida al paso', 1)

insert into Category(Name, SpanishName, ParentId)
values('Hotels', 'Hoteles', 6)

insert into Category(Name, SpanishName, ParentId)
values('Residencies', 'Residenciales', 6)

insert into Category(Name, SpanishName, ParentId)
values('Hostels', 'Hostales', 6)

insert into Category(Name, SpanishName, ParentId)
values('Public', 'Públicos', 7)

insert into Category(Name, SpanishName, ParentId)
values('Private', 'Privados', 7)

insert into Category(Name, SpanishName, ParentId)
values('Agencies', 'Agencias', 8)

insert into Category(Name, SpanishName, ParentId)
values('ATMS', 'ATMs', 8)

insert into Category(Name, SpanishName, ParentId)
values('Formal', 'Formales', 9)

insert into Category(Name, SpanishName, ParentId)
values('Informal', 'Informales', 9)

insert into Category(Name, SpanishName, ParentId)
values('Theaters', 'Teatros', 10)

insert into Category(Name, SpanishName, ParentId)
values('Museums', 'Museos', 10)


select * from Category where ParentId = 1