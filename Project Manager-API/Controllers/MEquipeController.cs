using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Manager_API.Conexão;
using Project_Manager_API.Models;

namespace Project_Manager_API.Controllers
{
    [ApiController]
    [Route("Api/Membros")]
    public class MEquipeController : ControllerBase
    {
        private readonly ContextoDoBanco _context;

        public MEquipeController(ContextoDoBanco context)
        {
            _context = context;
        }

        // Obtém todos os membros de equipe do projeto
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<MEquipe>>> ListarMembros()
        {
            try
            {
                var membros = await _context.Membros.ToListAsync();
                return Ok(membros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtém um membro pelo ID
        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<MEquipe>> BuscarMembro(int id)
        {
            try
            {
                var membro = await _context.Membros.FindAsync(id);

                if (membro == null)
                {
                    return NotFound();
                }

                return Ok(membro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Adiciona um membro
        [HttpPost("Criar")]
        public async Task<ActionResult<MEquipe>> CriarMembro(MEquipe membro)
        {
            try
            {
                _context.Membros.Add(membro);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(BuscarMembro), new { id = membro.ID }, membro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Atualiza um membro existente
        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> AtualizarMembro(int id, MEquipe membro)
        {
            try
            {
                if (id != membro.ID)
                {
                    return BadRequest();
                }

                _context.Entry(membro).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarMembro(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // Deleta um projeto pelo ID
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarMembro(int id)
        {
            try
            {
                var membro = await _context.Membros.FindAsync(id);

                if (membro == null)
                {
                    return NotFound();
                }
                _context.Membros.Remove(membro);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool VerificarMembro(int id)
        {
            try
            {
                return _context.Membros.Any(membro => membro.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar membro: " + ex.Message);
            }
        }


    }
}
