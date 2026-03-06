using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Request;
using api.DTOs.Response;
using api.Models;

namespace api.Mappers;

public static class AlunoMapper
{
    public static Aluno ToEntity(this AlunoRequestDTO request)
    {
        return new Aluno
        {
            Nome = request.Nome,
            Email = request.Email,
            TurmaId = request.TurmaId
        };
    }

    public static AlunoResponseDTO ToDTO(this Aluno entity)
    {
        return new AlunoResponseDTO(
            entity.Id,
            entity.Nome,
            entity.Email,
            entity.TurmaId
        );
    }
}
    
