using System;
using System.Collections.Generic;

namespace PagueVeloz.Domain
{
    public class EntityValidationException : Exception
    {
        public IList<string> Erros { get; private set; }
        public EntityValidationException(IList<string> erros) : base("Entidade inválida")
        {
            Erros = erros;
        }

    }
}
