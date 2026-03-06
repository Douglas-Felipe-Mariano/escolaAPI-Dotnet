using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models;

public class Turma
{
    public int Id { get; set; }
    public string Nome { get; set; }

     public List<Aluno> Alunos { get; set; }
}
