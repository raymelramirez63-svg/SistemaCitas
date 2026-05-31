namespace SistemaCitas.Services;

using SistemaCitas.Interfaces;
using SistemaCitas.Models;

public class CitaService(IClinicaRepositorio repositorio, INotificador notificador)
{
    public void AgendarCita(int pacienteId, int medicoId, DateTime fecha)
    {
        var paciente = repositorio.ObtenerPacientes().FirstOrDefault(p => p.Id == pacienteId) ?? throw new Exception("Paciente no existe.");
        var medico = repositorio.ObtenerMedicos().FirstOrDefault(m => m.Id == medicoId) ?? throw new Exception("Médico no existe.");

        if (fecha < DateTime.Now) throw new ArgumentException("No puedes agendar en el pasado.");
        if (repositorio.ExisteCitaConflicto(medico, fecha)) throw new InvalidOperationException("El médico está ocupado en ese horario.");

        var cita = new Cita { Paciente = paciente, Medico = medico, FechaHora = fecha };
        repositorio.GuardarCita(cita);

        notificador.EnviarMensaje(paciente.Email, $"Cita agendada con el Dr. {medico.Nombre} para el {fecha:g}.");
    }

    public List<Cita> ConsultarPorPaciente(int pacienteId) =>
        repositorio.ObtenerCitas().Where(c => c.Paciente.Id == pacienteId).ToList();

    public List<Cita> ConsultarPorMedico(int medicoId) =>
        repositorio.ObtenerCitas().Where(c => c.Medico.Id == medicoId).ToList();

    public void CancelarCita(int citaId)
    {
        var cita = repositorio.ObtenerCitas().FirstOrDefault(c => c.Id == citaId) ?? throw new Exception("Cita no encontrada.");
        cita.EstaCancelada = true;
        notificador.EnviarMensaje(cita.Paciente.Email, "Su cita ha sido cancelada.");
    }

    public void ReprogramarCita(int citaId, DateTime nuevaFecha)
    {
        var cita = repositorio.ObtenerCitas().FirstOrDefault(c => c.Id == citaId) ?? throw new Exception("Cita no encontrada.");
        if (cita.EstaCancelada) throw new Exception("La cita está cancelada.");
        if (nuevaFecha < DateTime.Now) throw new ArgumentException("Fecha inválida.");
        if (repositorio.ExisteCitaConflicto(cita.Medico, nuevaFecha)) throw new InvalidOperationException("Médico ocupado.");

        cita.FechaHora = nuevaFecha;
        notificador.EnviarMensaje(cita.Paciente.Email, $"Cita reprogramada para el {nuevaFecha:g}.");
    }
}