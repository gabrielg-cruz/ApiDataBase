namespace ApiBase.Models
{
    public class Pessoas
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public DateTime DtNasc { get; set; }
        public required string Email { get; set; }
        public int Atrasos { get; set; }
    }
}