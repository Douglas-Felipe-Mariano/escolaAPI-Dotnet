using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Request;
using api.DTOs.Response;
using api.Mappers;
using api.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace api.Services;

public class TurmaService
{
    private readonly string _connectionString;

    public TurmaService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
public async Task<IEnumerable<TurmaResponseDTO>> ListarTodasAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var turmas = await connection.QueryAsync<Turma>("SELECT * FROM Turmas");
        return turmas.Select(t => t.ToDTO());
    }

    public async Task<TurmaResponseDTO> CriarAsync(TurmaRequestDTO request)
    {
        var turma = request.ToEntity();
        using var connection = new SqlConnection(_connectionString);
        var sql = "INSERT INTO Turmas (Nome) VALUES (@Nome); SELECT CAST(SCOPE_IDENTITY() as int);";
        var id = await connection.QuerySingleAsync<int>(sql, turma);
        return new TurmaResponseDTO(id, turma.Nome);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync("DELETE FROM Turmas WHERE Id = @Id", new { Id = id });
        return rowsAffected > 0;
    }
}