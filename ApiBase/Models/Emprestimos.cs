namespace ApiBase.Models
{
    public class Emprestimos
    {
        public int Id { get; set; }
        public required int PessoaId { get; set; }
        public required int LivroId { get; set; }
        public DateTime DtEmprestimo { get; set; }
        public DateTime DtDevolucao { get; set; }
        public bool Atrasado { get; set; }
    }
}