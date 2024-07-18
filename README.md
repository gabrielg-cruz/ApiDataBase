# ApiDataBase

**ApiDataBase** é uma API RESTful projetada para gerenciar um sistema de biblioteca. Ela permite operações de CRUD (Criar, Ler, Atualizar e Deletar) para três entidades principais: Pessoas, Livros e Empréstimos.

## Índice

- [Visão Geral](#visão-geral)
- [Endpoints](#endpoints)
  - [Pessoas](#pessoas)
  - [Livros](#livros)
  - [Empréstimos](#empréstimos)
- [Exemplos de Requisições](#exemplos-de-requisições)
- [Erros Comuns](#erros-comuns)
- [Licença](#licença)

## Visão Geral

Esta API segue os princípios da arquitetura REST e permite a gestão de uma biblioteca com as seguintes funcionalidades:
- **Pessoas**: Gerenciamento de dados de usuários e funcionários da biblioteca.
- **Livros**: Cadastro e gerenciamento dos livros disponíveis na biblioteca.
- **Empréstimos**: Registro e acompanhamento dos empréstimos realizados.

## Endpoints

### Pessoas

- **GET /pessoas**: Lista todas as pessoas cadastradas.
- **GET /pessoas/{id}**: Recupera informações de uma pessoa específica.
- **GET /pessoas/{idade}**: Recupera informações de pessoas por idade.
- **GET /pessoas/{email}**: Recupera informações de pessoas por email.
- **GET /pessoas/{nome}**: Recupera informações de pessoas por nome.
- **POST /pessoas**: Cria uma nova pessoa.
- **PUT /pessoas/{id}**: Atualiza as informações de uma pessoa existente.
- **DELETE /pessoas/{id}**: Remove uma pessoa do cadastro.

### Livros

- **GET /livros**: Lista todos os livros cadastrados.
- **GET /livros/{id}**: Recupera informações de um livro específico.
- **GET /livrosEmprestados**: Recupera informações de livros emprestados.
- **GET /livrosDisponiveis**: Recupera informações de livros Disponiveis.
- **POST /livros**: Adiciona um novo livro.
- **PUT /livros/{id}**: Atualiza as informações de um livro existente.
- **DELETE /livros/{id}**: Remove um livro do catálogo.

### Empréstimos

- **GET /emprestimos**: Lista todos os empréstimos realizados.
- **GET /emprestimosAtrasados**: Lista todos os empréstimos realizados.
- **GET /emprestimosPorPessoa**: Lista todos os empréstimos ativos de uma pessoa através da junção de tabelas.
- **GET /emprestimos/{id}**: Recupera informações de um empréstimo específico.
- **POST /emprestimos**: Registra um novo empréstimo.
- **PUT /emprestimos/{id}**: Atualiza as informações de um empréstimo existente.
- **DELETE /emprestimos/{id}**: Cancela um empréstimo.

### Erros Comuns


- **404 Not Found**: Recurso não encontrado. Verifique se o ID está correto.
- **400 Bad Request**: Dados inválidos ou faltando. Verifique a estrutura do corpo da requisição.
- **500 Internal Server Error**: Erro no servidor. Consulte os logs para mais detalhes.

### Licença

- MIT License.

Copyright (c) 2024 Gabriel Gomes

Permissão é concedida, sem qualquer custo, a qualquer pessoa que obtenha uma cópia
deste software e dos arquivos de documentação associados (o "Software"), para lidar
com o Software sem restrição, incluindo sem limitação os direitos de usar, copiar,
modificar, mesclar, publicar, distribuir, sublicenciar e/ou vender cópias do
Software, desde que as seguintes condições sejam atendidas:

O aviso de copyright acima e este aviso de permissão devem ser incluídos em todas as
cópias ou partes substanciais do Software.

O Software é fornecido "no estado em que se encontra", sem garantia de qualquer tipo,
expressa ou implícita, incluindo, mas não se limitando às garantias implícitas de
comercialização, adequação a um propósito específico e não violação. Em nenhum
caso os autores ou detentores dos direitos autorais serão responsáveis por qualquer
reclamação, dano ou outra responsabilidade, seja em uma ação de contrato, ato ilícito
ou de outra forma, decorrente de, fora ou em conexão com o Software ou o uso ou
outras negociações no Software.


