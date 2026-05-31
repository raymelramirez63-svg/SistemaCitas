namespace SistemaCitas.Repositories;

using SistemaCitas.Interfaces;
using SistemaCitas.Modelos;
using SistemaCitas.Models;

public class ClinicaRepositorioMemoria : IClinicaRepositorio
{
    private readonly List<Paciente> _pacientes = new();
    private readonly List<Especialidad> _especialidades = new();
    private readonly List<Medico> _medicos = new();
    private readonly List<Cita> _citas = new();

    private int _pacienteId = 1, _especialidadId = 1, _medicoId = 1, _citaId = 1;

    public void GuardarPaciente(Paciente paciente) { paciente.Id = _pacienteId++; _pacientes.Add(paciente); }
    public List<Paciente> ObtenerPacientes() => _pacientes;

    public void GuardarEspecialidad(Especialidad especialidad) { especialidad.Id = _especialidadId++; _especialidades.Add(especialidad); }
    public List<Especialidad> ObtenerEspecialidades() => _especialidades;

    public void GuardarMedico(Medico medico) { medico.Id = _medicoId++; _medicos.Add(medico); }
    public List<Medico> ObtenerMedicos() => _medicos;

    public void GuardarCita(Cita cita) { cita.Id = _citaId++; _citas.Add(cita); }
    public List<Cita> ObtenerCitas() => _citas;

    public bool ExisteCitaConflicto(Medico medico, DateTime fecha)
    {
        return _citas.Any(c => c.Medico.Id == medico.Id
                            && c.FechaHora == fecha
                            && !c.EstaCancelada);
    }
}