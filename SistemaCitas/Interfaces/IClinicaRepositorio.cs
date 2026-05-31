namespace SistemaCitas.Interfaces;

using SistemaCitas.Modelos;
using SistemaCitas.Models;

public interface IClinicaRepositorio
{
    void GuardarPaciente(Paciente paciente);
    List<Paciente> ObtenerPacientes();

    void GuardarEspecialidad(Especialidad especialidad);
    List<Especialidad> ObtenerEspecialidades();

    void GuardarMedico(Medico medico);
    List<Medico> ObtenerMedicos();

    void GuardarCita(Cita cita);
    List<Cita> ObtenerCitas();
    bool ExisteCitaConflicto(Medico medico, DateTime fecha);
}