using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Manager_API.Conexão;
using Project_Manager_API.Models;

namespace Project_Manager_API.Controllers
{
    [ApiController]
    [Route("Api/Tarefas")]
    public class TarefaController : ControllerBase
    {
        private readonly ContextoDoBanco _context;

        public TarefaController(ContextoDoBanco context)
        {
            _context = context;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ListarTarefasProjeto()
        {
            try
            {
                var tarefas = await _context.Tarefas.ToListAsync();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Buscar/{id}")]
        async Task<ActionResult<Tarefa>> BuscarTarefa(int id)
        {
            try
            {
                var tarefas = await _context.Tarefas.FindAsync(id);

                if(tarefas == null)
                {
                    return NotFound();
                }

                return Ok(tarefas);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<Tarefa>> CriarTarefa(Tarefa tarefa)
        {
            try
            {
                _context.Tarefas.Add(tarefa);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(BuscarTarefa), new { id = tarefa.ID }, tarefa);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> AtualizarTarefa(int id, Tarefa tarefa)
        {
            try
            {
                if (id != tarefa.ID)
                {
                    return BadRequest();
                }

                _context.Entry(tarefa).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!VerificarTarefa(id))
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

        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            try
            {
                var tarefa = await _context.Tarefas.FindAsync(id);

                if(tarefa == null)
                {
                    return NotFound();
                }

                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();

                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool VerificarTarefa(int id)
        {
            try
            {
                return _context.Tarefas.Any(tarefa => tarefa.ID == id);
            }
            catch(Exception ex)
            {
                throw new Exception("erro ao verificar tarefa: " + ex.Message);
            }
        }

    }

}
