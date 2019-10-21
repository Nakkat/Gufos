using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route ("api/[Controller]")]
    [ApiController]
    public class EventosController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        
        // GET: api/Eventos
        [HttpGet]
        public async Task<ActionResult<List<Eventos>>> Get () {

           //Include("") = Adciona efetivamente a árvore de objetos relacionados
 var Eventos = await _contexto.Eventos.Include("Categoria").Include("Localizacao").ToListAsync();

            if (Eventos == null) {
                return NotFound ();
            }
            return Eventos;
        }
        
        
        // GET: api/Eventos/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Eventos>> Get (int id) {

            var Eventos = await _contexto.Eventos.Include("Categoria").Include("Localizacao").FirstOrDefaultAsync(e => e.IdEvento == id);

            if (Eventos == null) {
                return NotFound ();
            }
            return Eventos;
        }

        // POST api/Categora
        [HttpPost]
        public async Task<ActionResult<Eventos>> Post (Eventos Eventos) {
            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (Eventos);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return Eventos;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put (int id, Eventos Eventos) {
            
            // Se o Id do objeto não existir
            // ele retorna o erro 400
            if (id != Eventos.IdEvento){
                return BadRequest();
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(Eventos).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync(); 
            } catch(DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco

                var Eventos_valido = await _contexto.Eventos.FindAsync(id);

                if(Eventos_valido == null) {
                    return NotFound();
                }else{
                    throw;
                }
            }
                return NoContent();
        }

        // Delete api/Eventos/id

        [HttpDelete("{id}")]
        public async Task<ActionResult<Eventos>> Delete(int id){

            var Eventos = await _contexto.Eventos.FindAsync(id);
            if(Eventos == null){
                return NotFound();
            }

            _contexto.Eventos.Remove(Eventos);
            
            await _contexto.SaveChangesAsync();

            return Eventos; 
        }
    }
}