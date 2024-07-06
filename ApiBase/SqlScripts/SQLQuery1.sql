CREATE TABLE Pessoa (
 Id int Identity (1,1) Not Null,
 Nome VARCHAR(30) NOT NULL,
 DtNasc int,
 Email varchar(30)Not Null	
 constraint PK_Pessoa PRIMARY KEY(Email)
);