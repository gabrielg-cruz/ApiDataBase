namespace ApiBase.Models
{
    public class Pessoas
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Email { get; set; }


    }
}