using Project_Manager_API.Models;
using Project_Manager_API.Repositories;

namespace Project_Manager_API.Services
{
    public class ProjetoService
    {
        private readonly ProjetoRepository _projetoRepository;

        public ProjetoService(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task<IEnumerable<Projeto>> ListarProjetos()
        {
            return await _projetoRepository.ListarProjetos();
        }

        public async Task<Projeto> BuscarProjeto(int id)
        {
            return await _projetoRepository.BuscarProjeto(id);
        }

        public async Task<Projeto> CriarProjeto(Projeto projeto)
        {
            return await _projetoRepository.CriarProjeto(projeto);
        }

        public async Task<Projeto> AtualizarProjeto(int id, Projeto projeto)
        {
            return await _projetoRepository.AtualizarProjeto(id, projeto);
        }

        public async Task<Projeto> DeletarProjeto(int id)
        {
            return await _projetoRepository.DeletarProjeto(id);
        }
    }
}
