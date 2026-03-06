using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Request;
using api.DTOs.Response;
using Dapper;
using Microsoft.Data.SqlClient;
using api.Mappers;
using api.Models;

namespace api.Services;

public class AlunoService(IConfiguration configuration)
{
    public readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<AlunoResponseDTO> CriarAlunoAsync(AlunoRequestDTO request)
    {
        var aluno = request.ToEntity();

        using var connection = new SqlConnection(_connectionString);
        var sql = @"INSERT INTO Alunos (Nome, Email, TurmaId) 
                    VALUES (@Nome, @Email, @TurmaId);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
        
        var id = await connection.QuerySingleAsync<int>(sql, aluno);
        aluno.Id = id;

        return aluno.ToDTO();
    }
    public async Task<IEnumerable<AlunoResponseDTO>> ListarTodosAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Alunos";
        var alunos = await connection.QueryAsync<Aluno>(sql);
        
        return alunos.Select(a => a.ToDTO());
    }

    public async Task<AlunoResponseDTO?> ObterPorIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM Alunos WHERE Id = @Id";
        var aluno = await connection.QueryFirstOrDefaultAsync<Aluno>(sql, new { Id = id });
        
        return aluno?.ToDTO();
    }

    public async Task<bool> AtualizarAsync(int id, AlunoRequestDTO request)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"UPDATE Alunos 
                    SET Nome = @Nome, Email = @Email, TurmaId = @TurmaId 
                    WHERE Id = @Id";
        
        // Criamos um objeto anónimo para o Dapper mapear o @Id vindo da URL
        var rowsAffected = await connection.ExecuteAsync(sql, new { 
            request.Nome, 
            request.Email, 
            request.TurmaId, 
            Id = id 
        });

        return rowsAffected > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "DELETE FROM Alunos WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        
        return rowsAffected > 0;
    }
}