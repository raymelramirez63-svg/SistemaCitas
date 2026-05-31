namespace SistemaCitas.Services;

using SistemaCitas.Interfaces;
using SistemaCitas.Modelos;
using SistemaCitas.Models;
using SistemaCitas.Utils; 
using System.ComponentModel.DataAnnotations;

public class ClinicaService(IClinicaRepositorio repositorio)
{
    public void RegistrarPaciente(string nombre, string email)
    {
        Validador.ValidarTexto(nombre, "Nombre del Paciente");
        Validador.ValidarTexto(email, "Email del Paciente");
        repositorio.GuardarPaciente(new Paciente { Nombre = nombre, Email = email });
    }

    public void RegistrarEspecialidad(string nombre)
    {
        Validador.ValidarTexto(nombre, "Nombre de la Especialidad");
        repositorio.GuardarEspecialidad(new Especialidad { Nombre = nombre });
    }

    public void RegistrarMedico(string nombre, int especialidadId)
    {
        Validador.ValidarTexto(nombre, "Nombre del Médico");

        var especialidad = repositorio.ObtenerEspecialidades().FirstOrDefault(e => e.Id == especialidadId)
                           ?? throw new Exception("Especialidad no encontrada.");

        repositorio.GuardarMedico(new Medico { Nombre = nombre, Especialidad = especialidad });
    }

    public List<Paciente> ObtenerPacientes() => repositorio.ObtenerPacientes();
    public List<Medico> ObtenerMedicos() => repositorio.ObtenerMedicos();
    public List<Especialidad> ObtenerEspecialidades() => repositorio.ObtenerEspecialidades();
}