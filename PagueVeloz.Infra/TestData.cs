using System;
using PagueVeloz.Domain;
using PagueVeloz.Domain.Entities;
using PagueVeloz.Domain.Interfaces;

namespace PagueVeloz.Infra
{
    public class TestData
    {
        private static bool _dataAdded;
        public static void AddTestData(IRepository repository)
        {
            if(_dataAdded) return;
            _dataAdded = true;
            var empresa1 = new Empresa("Empresa 1", "SC", "53.076.604/0001-88");
            var empresa2 = new Empresa("Empresa 2", "PR", "54.474.404/4041-48");

            var fornecedor1 = new Fornecedor("13.011.114/0001-11", "Fornecedor PJ");
            var fornecedor2 = new Fornecedor("011.353.769-41", "3816.89-5", new DateTime(1988,01,01), "Fornecedor PF");

            fornecedor1.AdicionarTelefone("2222-5555");
            fornecedor1.AdicionarTelefone("1111-5555");
            fornecedor1.AdicionarTelefone("1111-1111");
            fornecedor2.AdicionarTelefone("0000-1111");

            fornecedor1.Empresa = empresa1;
            fornecedor2.Empresa = empresa2;

            repository.SaveOrUpdate(empresa1);
            repository.SaveOrUpdate(empresa2);
            repository.SaveOrUpdate(fornecedor1);
            repository.SaveOrUpdate(fornecedor2);
        }
    }
}
