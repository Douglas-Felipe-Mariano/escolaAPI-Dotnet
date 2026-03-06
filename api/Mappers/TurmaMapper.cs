using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Request;
using api.DTOs.Response;
using api.Models;

namespace api.Mappers;

public static class TurmaMapper
{
    public static Turma ToEntity(this TurmaRequestDTO request)
    {
        return new Turma {Nome = request.Nome};
    }

    public static TurmaResponseDTO ToDTO(this Turma entity)
    {
        return new TurmaResponseDTO(entity.Id, entity.Nome );
    }
}
