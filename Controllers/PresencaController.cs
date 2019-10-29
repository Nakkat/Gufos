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
    public class PresencaController : ControllerBase
    {
        PresencaRepository repositorio = new PresencaRepository();


        // GET: api/Presenca/
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get()
        {
            var presenca = await repositorio.Listar();

            if (presenca == null)
            {
                return NotFound();
            }

            return presenca;
        }

        // GET: api/Presenca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id)
        {
            var presenca = await repositorio.BuscarPorID(id);

            if (presenca == null)
            {
                return NotFound();
            }

            return presenca;
        }

        // POST: api/Presenca/
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca)
        {
            try
            {
                await repositorio.Salvar(presenca);
                return presenca;
            }
            catch (DbUpdateConcurrencyException)
            {
                 return BadRequest();
            }
        }        


        // PUT: api/Presenca/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Presenca presenca)
        {
            if (id != presenca.IdPresenca)
            {
                return BadRequest();
            }

           

            try
            {
                await repositorio.Alterar(presenca);
            }
            catch (DbUpdateConcurrencyException)
            {
                 var presenca_valido = repositorio.BuscarPorID(id);

                if (presenca_valido == null)
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

        // DELETE: api/Presenca/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id)
        {
            var presenca = await repositorio.BuscarPorID(id);
            if (presenca == null)
            {
                return NotFound();
            }

            presenca = await repositorio.Excluir(presenca);

            return presenca;
        }



    }
}