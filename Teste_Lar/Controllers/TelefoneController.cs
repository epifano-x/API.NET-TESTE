﻿using Microsoft.AspNetCore.Mvc;
using Teste_Lar.Models;
using Teste_Lar.Models.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Teste_Lar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelefoneController : ControllerBase
    {
        private readonly ITelefoneRepository _telefoneRepository;

        public TelefoneController(ITelefoneRepository telefoneRepository)
        {
            _telefoneRepository = telefoneRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefone>>> Get()
        {
            var telefones = await _telefoneRepository.GetAllAsync();
            return Ok(telefones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Telefone>> Get(int id)
        {
            var telefone = await _telefoneRepository.GetByIdAsync(id);
            if (telefone == null)
            {
                return NotFound();
            }
            return Ok(telefone);
        }

        [HttpPost]
        public async Task<ActionResult<Telefone>> Post([FromBody] Telefone telefone)
        {
            await _telefoneRepository.CreateAsync(telefone);
            return CreatedAtAction(nameof(Get), new { id = telefone.Id }, telefone);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Telefone telefone)
        {
            if (id != telefone.Id)
            {
                return BadRequest();
            }

            await _telefoneRepository.UpdateAsync(telefone);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var telefone = await _telefoneRepository.GetByIdAsync(id);
            if (telefone == null)
            {
                return NotFound();
            }

            await _telefoneRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
