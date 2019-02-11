using System.Collections.Generic;

namespace PagueVeloz.Domain.DTO
{
    public class EmpresaDto
    {
        public long Id { get; set; }
        public string NomeFantasia { get; set; }
        public string Uf { get; set; }
        public string Cnpj { get; set; }
    }
}
