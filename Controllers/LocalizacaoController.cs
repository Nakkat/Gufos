using System.Collections.Generic;
using System.Threading.Tasks;
using GUFOS_BackEnd.Domains;
using GUFOS_BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GUFOS_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        LocalizacaoRepository repositorio = new LocalizacaoRepository();


        // GET: api/Localizacao/
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get()
        {
            var localizacao = await repositorio.Listar();

            if (localizacao == null)
            {
                return NotFound();
            }

            return localizacao;
        }

        // GET: api/Localizacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id)
        {
            var localizacao = await repositorio.BuscarPorID(id);

            if (localizacao == null)
            {
                return NotFound();
            }

            return localizacao;
        }

        // POST: api/Localizacao/
        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao)
        {
            try
            {
                await repositorio.Salvar(localizacao);
                return localizacao;
            }
            catch (DbUpdateConcurrencyException)
            {
                 return BadRequest();
            }
        }        


        // PUT: api/Localizacao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Localizacao localizacao)
        {
            if (id != localizacao.IdLocalizacao)
            {
                return BadRequest();
            }

           

            try
            {
                await repositorio.Alterar(localizacao);
            }
            catch (DbUpdateConcurrencyException)
            {
                 var localizacao_valido = repositorio.BuscarPorID(id);

                if (localizacao_valido == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Localizacao/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id)
        {
            var localizacao = await repositorio.BuscarPorID(id);
            if (localizacao == null)
            {
                return NotFound();
            }

            localizacao = await repositorio.Excluir(localizacao);

            return localizacao;
        }



    }
}