using Microsoft.EntityFrameworkCore;
using Project_Manager_API.Conexão;
using Project_Manager_API.Models;
using System;

namespace Project_Manager_API.Repositories
{
    public class ProjetoRepository
    {
        private readonly ContextoDoBanco _context;

        public ProjetoRepository(ContextoDoBanco context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Projeto>> ListarProjetos()
        {
            return await _context.Projetos.ToListAsync();
        }

        public async Task<Projeto> BuscarProjeto(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                throw new Exception("Projeto não encontrado");
            }
            return projeto;
        }

        public async Task<Projeto> CriarProjeto(Projeto projeto)
        {
            if (projeto == null)
            {
                throw new ArgumentNullException(nameof(projeto));
            }
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return projeto;
        }

        public async Task<Projeto> AtualizarProjeto(int id, Projeto projeto)
        {
            if (projeto == null)
            {
                throw new ArgumentNullException(nameof(projeto));
            }
            _context.Entry(projeto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarProjeto(id))
                {
                    throw new Exception("Projeto não encontrado");
                }
                else
                {
                    throw;
                }
            }

            return projeto;
        }

        public async Task<Projeto> DeletarProjeto(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                throw new Exception("Projeto não encontrado");
            }

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return projeto;
        }

        private bool VerificarProjeto(int id)
        {
            return _context.Projetos.Any(e => e.ID == id);
        }
    }
}
