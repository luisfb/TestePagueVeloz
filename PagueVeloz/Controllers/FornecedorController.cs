using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PagueVeloz.Domain;
using PagueVeloz.Domain.DTO;
using PagueVeloz.Domain.Interfaces;
using PagueVeloz.Infra;

namespace PagueVeloz.Controllers
{
    [Route("api/[controller]")]
    public class FornecedorController : Controller
    {
        private ICadastroService _cadastros;
        public FornecedorController(ICadastroService cadastros, IServiceProvider services)
        {
            TestData.AddTestData((IRepository)services.GetService(typeof(IRepository)));
            _cadastros = cadastros;
        }

        [HttpGet]
        public IActionResult Get(FiltroDto filtros)
        {
            try
            {
                return Ok(_cadastros.ListarFornecedores(filtros));
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_cadastros.ListarFornecedores(new FiltroDto{IdFornecedor = id}));
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]FornecedorDto value)
        {
            try
            {
                _cadastros.CadastrarFornecedor(value);
                return Ok();
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]FornecedorDto value)
        {
            try
            {
                _cadastros.AlterarFornecedor(id, value);
                return Ok();
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cadastros.DeletarFornecedor(id);
                return Ok();
            }
            catch (EntityValidationException e)
            {
                return BadRequest(e.Erros);
            }
        }
    }
}
