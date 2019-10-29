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
    public class EventoController : ControllerBase
    {
        // Depois que incluímos nosso repositório no controller, desabilitamos qualquer tipo de comunicação direta com o contexto
        //GufosContext _context = new GufosContext();
        EventoRepository repositorio = new EventoRepository();

        // GET: api/Evento/
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get()
        {
            var eventos = await repositorio.Listar();

            if (eventos == null)
            {
                return NotFound();
            }

            return eventos;
        }

        // GET: api/Evento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {
            var evento = await repositorio.BuscarPorID(id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        // POST: api/Evento/
        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento)
        {
            try
            {
                await repositorio.Salvar(evento);
                return evento;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            
        }        


        // PUT: api/Evento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return BadRequest();
            }

            try
            {
                await repositorio.Alterar(evento);
            }
            catch (DbUpdateConcurrencyException)
            {
                var evento_valido = repositorio.BuscarPorID(id);

                if (evento_valido == null)
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

        // DELETE: api/Evento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> Delete(int id)
        {
            var evento = await repositorio.BuscarPorID(id);
            if (evento == null)
            {
                return NotFound();
            }

            evento = await repositorio.Excluir(evento);

            return evento;
        }



    }
}