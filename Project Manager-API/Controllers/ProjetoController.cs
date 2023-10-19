using Microsoft.AspNetCore.Mvc;
using Project_Manager_API.Models;
using Project_Manager_API.Services;
using System;

namespace Project_Manager_API.Controllers
{
    [ApiController]
    [Route("Api/Projetos")]
    public class ProjetoController : ControllerBase
    {
        private readonly ProjetoService _projetoService;

        public ProjetoController(ProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        // Métodos públicos

        // Obtém todos os projetos
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Projeto>>> ListarProjetos()
        {
            var projetos = await _projetoService.ListarProjetos();
            return Ok(projetos);
        }

        // Obtém um projeto pelo ID
        [HttpGet("Buscar/{id}")]
        public async Task<ActionResult<Projeto>> BuscarProjeto(int id)
        {
            var projeto = await _projetoService.BuscarProjeto(id);

            if (projeto == null)
            {
                return NotFound();
            }

            return Ok(projeto);
        }

        // Adiciona um novo projeto
        [HttpPost("Criar")]
        public async Task<ActionResult<Projeto>> CriarProjeto(Projeto projeto)
        {
            if (ProjetoValido(projeto))
            {
                var projetoCriado = await _projetoService.CriarProjeto(projeto);
                return CreatedAtAction(nameof(BuscarProjeto), new { id = projetoCriado.ID }, projetoCriado);
            }

            return BadRequest("Os campos do projeto não podem estar vazios ou nulos.");
        }

        // Atualiza um projeto existente
        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> AtualizarProjeto(int id, Projeto projeto)
        {
            if (ProjetoValido(projeto))
            {
                var projetoAtualizado = await _projetoService.AtualizarProjeto(id, projeto);
                if (projetoAtualizado == null)
                {
                    return NotFound();
                }

                return NoContent();
            }

            return BadRequest("Os campos do projeto não podem estar vazios ou nulos.");
        }

        // Deleta um projeto pelo ID
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarProjeto(int id)
        {
            var projetoDeletado = await _projetoService.DeletarProjeto(id);
            if (projetoDeletado == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Métodos privados

        private bool ProjetoValido(Projeto projeto)
        {
            return !(string.IsNullOrEmpty(projeto.Nome) || 
                     string.IsNullOrEmpty(projeto.Descricao) || 
                     projeto.Status == null || 
                     projeto.DataInicio == DateTime.MinValue || 
                     projeto.DataTermino == DateTime.MinValue);
        }
    }
}
