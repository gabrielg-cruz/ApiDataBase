CREATE TABLE Pessoa (
 Id int Identity (1,1) Not Null,
 Nome VARCHAR(30) NOT NULL,
 DtNasc DateTime,
 Email varchar(30)Not Null	
 constraint PK_Pessoa PRIMARY KEY(Id)
);
CREATE TABLE Livro (
 Id int Identity (1,1) NOT NULL,
 Titulo VARCHAR(30) NOT NULL,
 Editora VARCHAR(30) NOT NULL,
 Capa VARCHAR(20)NOT NULL,
 Estado VARCHAR(10) NOT NULL,
 DataPubl DateTime
 constraint PK_Livro PRIMARY KEY(Id)
);

CREATE TABLE Emprestimo (
 Id int Identity (1,1) Not Null,
 PessoaId int NOT NULL,
 LivroId int NOT NULL,
 DtEmprestimo DateTime NOT NULL,
 DtDevolucao DateTime NOT NULL,
 Atrasado bit NOT NULL,
 constraint PK_Emprestimos PRIMARY KEY(Id),
 constraint FK_PessoaId Foreign KEY(PessoaId) references Pessoa(Id),
 constraint FK_LivroId Foreign KEY(LivroId) references Livro(Id)
);
