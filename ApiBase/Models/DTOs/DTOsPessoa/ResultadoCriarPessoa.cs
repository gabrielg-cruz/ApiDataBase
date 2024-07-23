using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBase.Models.DTOs
{
    public class ResultadoCriarPessoa
    {
        public Pessoas Pessoa { get; set; }
        public string? Erro { get; set; }
        public bool Sucesso => string.IsNullOrEmpty(Erro);
    }
}