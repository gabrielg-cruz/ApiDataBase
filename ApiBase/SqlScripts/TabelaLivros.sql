CREATE TABLE Livros (
 Id int Identity (1,1) NOT NULL,
 Titulo VARCHAR(30) NOT NULL,
 Editora VARCHAR(30) NOT NULL,
 Capa VARCHAR(10)NOT NULL,
 Estado int NOT NULL,
 DataPubl DateTime
 constraint PK_Livro PRIMARY KEY(Id)
);