using System;
using System.Collections.Generic;
using System.Text;
namespace SistemaCitas.Models;

public class Medico
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public Especialidad? Especialidad { get; set; }
}