using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Request;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    
    private readonly AlunoService _alunoService;

    public AlunoController(AlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAluno([FromBody] AlunoRequestDTO request)
    {
        var aluno = await _alunoService.CriarAlunoAsync(request);
        return CreatedAtAction(nameof(CriarAluno), new { id = aluno.Id }, aluno);
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodosAsync()
    {
        var alunos = await _alunoService.ListarTodosAsync();
        return Ok(alunos);
    }
    
    [HttpGet("/api/Aluno/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var aluno = await _alunoService.ObterPorIdAsync(id);
        return aluno == null ? NotFound("Aluno não encontrado.") : Ok(aluno);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AlunoRequestDTO request)
    {
        var sucesso = await _alunoService.AtualizarAsync(id, request);
        return sucesso ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var sucesso = await _alunoService.EliminarAsync(id);
        return sucesso ? NoContent() : NotFound();
    }
}