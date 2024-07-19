using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBase.Services.Interfaces
{
    public interface IPessoasService
    {
        int CalcularIdade(DateTime dtNasc);
    }
}