CREATE DATABASE GestioneSpese

create table CategoriaSpesa(
IDCategoria int identity(1,1) constraint PK_IDCategoria primary key,
Nome varchar(100)

);

create table Spesa(
ID int identity (1,1) constraint PK_ID primary key,
Insert_date datetime,
Description varchar(500),
Utente varchar(100),
Importo decimal(10,2) constraint check_Importo check (Importo>=0),
Approvata bit,
categoria int foreign key references CategoriaSpesa(IDCategoria),
);


insert into CategoriaSpesa values ('Shopping');
insert into CategoriaSpesa values ('Affitto');
insert into CategoriaSpesa values ('Lezioni Inglese');
--insert into CategoriaSpesa values ('Bollette Acqua e Luce');
--insert into CategoriaSpesa values ('Spesa Cibo');
--insert into CategoriaSpesa values ('Spesa Detersivi');

insert into Spesa values ('2021-12-23', 'Maglioncino Zara', 'Maria Rossi', 53.24, 0, 1);
insert into Spesa values ('2021-12-24', 'Mensile Luglio', 'Alessia Franceschini', 1200, 1, 2);

