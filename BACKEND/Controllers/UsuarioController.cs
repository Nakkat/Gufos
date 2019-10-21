using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers {
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route ("api/[Controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        GufosContext _contexto = new GufosContext ();

        
        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get () {

            var Usuarios = await _contexto.Usuario.ToListAsync ();

            if (Usuarios == null) {
                return NotFound ();
            }
            return Usuarios;
        }
        
        
        // GET: api/Usuario/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Usuario>> Get (int id) {

            var Usuario = await _contexto.Usuario.FindAsync (id);

            if (Usuario == null) {
                return NotFound ();
            }
            return Usuario;
        }

        // POST api/Categora
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post (Usuario usuario) {
            try {
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (usuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put (int id, Usuario usuario) {
            
            // Se o Id do objeto não existir
            // ele retorna o erro 400
            if (id != usuario.IdUsuario){
                return BadRequest();
            }
            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(usuario).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync(); 
            } catch(DbUpdateConcurrencyException) {

                // Verificamos se o objeto inserido realmente existe no banco

                var usuario_valido = await _contexto.Usuario.FindAsync(id);

                if(usuario_valido == null) {
                    return NotFound();
                }else{
                    throw;
                }
            }
                return NoContent();
        }

        // Delete api/usuario/id

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){

            var usuario = await _contexto.Usuario.FindAsync(id);
            if(usuario == null){
                return NotFound();
            }

            _contexto.Usuario.Remove(usuario);
            
            await _contexto.SaveChangesAsync();

            return usuario; 
        }
    }
}