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
public class TurmaController : ControllerBase
{   
    private readonly TurmaService _turmaService;

    public TurmaController(TurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _turmaService.ListarTodasAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TurmaRequestDTO request)
    {
        var result = await _turmaService.CriarAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucesso = await _turmaService.EliminarAsync(id);
        return sucesso ? NoContent() : NotFound();
    }
}