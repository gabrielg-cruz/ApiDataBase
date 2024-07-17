using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBase.Models
{
    public class Livros
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Editora { get; set; }
        public required string Capa { get; set; }
        public LivroEstado Estado { get; set; }
        public DateTime DataPubl { get; set; }
    }
}