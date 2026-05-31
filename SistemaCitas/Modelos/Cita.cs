using SistemaCitas.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCitas.Models;

public class Cita
{
    public int Id { get; set; }
    public Paciente Paciente { get; set; } = new();
    public Medico Medico { get; set; } = new();
    public DateTime FechaHora { get; set; }
    public bool EstaCancelada { get; set; } = false;
}