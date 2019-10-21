using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route ("api/[Controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        
        // GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get () {

            var TipoUsuarios = await _contexto.TipoUsuario.ToListAsync ();

            if (TipoUsuarios == null) {
                return NotFound ();
            }
            return TipoUsuarios;
        }
        
        
        // GET: api/TipoUsuario/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get (int id) {

            var TipoUsuario = await _contexto.TipoUsuario.FindAsync (id);

            if (TipoUsuario == null) {
                return NotFound ();
            }
            return TipoUsuario;
        }

        // POST api/Categora
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post (TipoUsuario tipousuario) {
            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (tipousuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return tipousuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put (int id, TipoUsuario tipousuario) {
            
            // Se o Id do objeto não existir
            // ele retorna o erro 400
            if (id != tipousuario.IdTipoUsuario){
                return BadRequest();
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(tipousuario).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync(); 
            } catch(DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco

                var tipousuario_valido = await _contexto.TipoUsuario.FindAsync(id);

                if(tipousuario_valido == null) {
                    return NotFound();
                }else{
                    throw;
                }
            }
                return NoContent();
        }

        // Delete api/tipousuario/id

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id){

            var tipousuario = await _contexto.TipoUsuario.FindAsync(id);
            if(tipousuario == null){
                return NotFound();
            }

            _contexto.TipoUsuario.Remove(tipousuario);
            
            await _contexto.SaveChangesAsync();

            return tipousuario; 
        }
    }
}