using System;
using Microsoft.AspNetCore.Mvc;
using PagueVeloz.Domain;
using PagueVeloz.Domain.DTO;
using PagueVeloz.Domain.Interfaces;
using PagueVeloz.Infra;

namespace PagueVeloz.Controllers
{
    [Route("api/[controller]")]
    public class EmpresaController : Controller
    {
        private ICadastroService _cadastros;
        public EmpresaController(ICadastroService cadastros, IServiceProvider services)
        {
            TestData.AddTestData((IRepository)services.GetService(typeof(IRepository)));
            _cadastros = cadastros;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_cadastros.ListarEmpresas());
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]EmpresaDto value)
        {
            try
            {
                _cadastros.CadastrarEmpresa(value);
                return Ok();
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]EmpresaDto value)
        {
            try
            {
                _cadastros.AlterarEmpresa(id,value);
                return Ok();
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }
    }
}
