use w1740485_CebritasDb
go

insert into [Role](Name) values('admin')
go
insert into [Role](Name) values('visitor')
go

insert into Usuario(Email, Name, [Password],AuthenticationCode, ActivationCode, Active, RoleId)
values('danieloo_123@hotmail.com', 'Sergio Guillen Mantilla', '71f8e7976e4cbc4561c9d62fb283e7f788202acb',
'49c63b76cc5c878f7936f68626224fc7d73d78f3', 'b62181b2ebb5752d0015582d673aa8ae', (1), 1)
go

insert into AccessToken(Token, UserId) values('a9e91859c26fddd5ec8b47284b752abe', 1)
go

/*
reporte1:  -16.485298,-68.122066
reporte2:  -16.485223,-68.121403
reporte3:  -16.488158,-68.122401
*/

insert into Precio(FourSquareFirstCategoryId, FourSquareVenueId, 
                   MinPrice, MaxPrice, Capacity, Parking, Holidays,
                   SmokingArea, KidsArea, Delivery)
values ('2d001558236f68626224fc', '76cc5c87284b752ab',
        25, 25, 200, (1), (1), (0), (0), (0))
        
        
insert into Precio(FourSquareFirstCategoryId, FourSquareVenueId, 
                   MinPrice, MaxPrice, Capacity, Parking, Holidays,
                   SmokingArea, KidsArea, Delivery)
values ('2ab2390faaec78236f68626224fc', '76cc236f686262ab',
        10, 200, 1000, (1), (1), (0), (1), (1))       
