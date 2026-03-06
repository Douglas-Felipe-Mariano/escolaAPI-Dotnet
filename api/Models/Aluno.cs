using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public String Email { get; set; }

    public int TurmaId { get; set; }   
}
