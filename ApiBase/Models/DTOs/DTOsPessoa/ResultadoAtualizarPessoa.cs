using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBase.Models.DTOs
{
    public class ResultadoAtualizarPessoa
    {
        public bool Sucesso { get; set; }
        public string? Erro { get; set; }
    }
}