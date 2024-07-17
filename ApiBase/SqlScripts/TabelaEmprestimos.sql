CREATE TABLE Emprestimos (
 Id int Identity (1,1) Not Null,
 PessoaId int NOT NULL,
 LivroId int NOT NULL,
 DtEmprestimo DateTime NOT NULL,
 DtDevolucao DateTime NOT NULL,
 Atrasado bit NOT NULL,
 constraint PK_Emprestimos PRIMARY KEY(Id),
 constraint FK_PessoaId Foreign KEY(PessoaId) references Pessoa(Id),
 constraint FK_LivroId Foreign KEY(LivroId) references Livros(Id)
);