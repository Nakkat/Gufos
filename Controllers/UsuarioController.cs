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
    public class UsuarioController : ControllerBase
    {
        UsuarioRepository repositorio = new UsuarioRepository();


        // GET: api/Usuario/
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuario = await repositorio.Listar();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await repositorio.BuscarPorID(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuario/
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            try
            {
                await repositorio.Salvar(usuario);
                return usuario;
            }
            catch (DbUpdateConcurrencyException)
            {
                 return BadRequest();
            }
        }        


        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

           

            try
            {
                await repositorio.Alterar(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                 var usuario_valido = repositorio.BuscarPorID(id);

                if (usuario_valido == null)
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

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var usuario = await repositorio.BuscarPorID(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario = await repositorio.Excluir(usuario);

            return usuario;
        }



    }
}