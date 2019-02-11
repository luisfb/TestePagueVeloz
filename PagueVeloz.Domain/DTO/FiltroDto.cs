using System;

namespace PagueVeloz.Domain.DTO
{
    public class FiltroDto
    {
        public long IdFornecedor { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataDeCadastroInicial { get; set; }
        public DateTime? DataDeCadastroFinal { get; set; }
    }
}
