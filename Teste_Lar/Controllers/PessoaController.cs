using Microsoft.AspNetCore.Mvc;
using Teste_Lar.Models;
using Teste_Lar.Models.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Teste_Lar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
        {
            var pessoas = await _pessoaRepository.GetAllAsync();
            return Ok(pessoas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> Get(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Pessoa>> Post([FromBody] Pessoa pessoa)
        {
            await _pessoaRepository.CreateAsync(pessoa);
            return CreatedAtAction(nameof(Get), new { id = pessoa.Id }, pessoa);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            await _pessoaRepository.UpdateAsync(pessoa);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            await _pessoaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
