using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Manager_API.Conexão;
using Project_Manager_API.Models;

namespace Project_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MEquipe_TarefaController : ControllerBase
    {
        private readonly ContextoDoBanco _context;

        public MEquipe_TarefaController(ContextoDoBanco context)
        {
            _context = context;
        }

        // GET: api/MEquipe_Tarefa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MEquipe_Tarefa>>> GetMEquipe_Tarefas()
        {
            return await _context.MEquipe_Tarefas.ToListAsync();
        }

        // GET: api/MEquipe_Tarefa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MEquipe_Tarefa>> GetMEquipe_Tarefa(int id)
        {
            var mEquipe_Tarefa = await _context.MEquipe_Tarefas.FindAsync(id);

            if (mEquipe_Tarefa == null)
            {
                return NotFound();
            }

            return mEquipe_Tarefa;
        }

        // PUT: api/MEquipe_Tarefa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMEquipe_Tarefa(int id, MEquipe_Tarefa mEquipe_Tarefa)
        {
            if (id != mEquipe_Tarefa.ID)
            {
                return BadRequest();
            }

            _context.Entry(mEquipe_Tarefa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MEquipe_TarefaExists(id))
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

        // POST: api/MEquipe_Tarefa
        [HttpPost]
        public async Task<ActionResult<MEquipe_Tarefa>> PostMEquipe_Tarefa(MEquipe_Tarefa mEquipe_Tarefa)
        {
            _context.MEquipe_Tarefas.Add(mEquipe_Tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMEquipe_Tarefa", new { id = mEquipe_Tarefa.ID }, mEquipe_Tarefa);
        }

        // DELETE: api/MEquipe_Tarefa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMEquipe_Tarefa(int id)
        {
            var mEquipe_Tarefa = await _context.MEquipe_Tarefas.FindAsync(id);
            if (mEquipe_Tarefa == null)
            {
                return NotFound();
            }

            _context.MEquipe_Tarefas.Remove(mEquipe_Tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MEquipe_TarefaExists(int id)
        {
            return _context.MEquipe_Tarefas.Any(e => e.ID == id);
        }
    }
}
