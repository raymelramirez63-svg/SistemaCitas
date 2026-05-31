namespace SistemaCitas.Menus;

using SistemaCitas.Models;
using SistemaCitas.Services;

public class MenuPrincipal(ClinicaService clinicaService, CitaService citaService)
{
    public void Mostrar()
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE CITAS MÉDICAS ===");
            Console.WriteLine("1. Registrar Paciente");
            Console.WriteLine("2. Registrar Especialidad");
            Console.WriteLine("3. Registrar Médico (y asignar especialidad)");
            Console.WriteLine("4. Agendar Cita");
            Console.WriteLine("5. Consultar Citas por Paciente");
            Console.WriteLine("6. Consultar Citas por Médico");
            Console.WriteLine("7. Reprogramar Cita");
            Console.WriteLine("8. Cancelar Cita");
            Console.WriteLine("9. Salir");
            Console.Write("\nSeleccione una opción: ");

            try
            {
                switch (Console.ReadLine())
                {
                    case "1": RegistrarPaciente(); break;
                    case "2": RegistrarEspecialidad(); break;
                    case "3": RegistrarMedico(); break;
                    case "4": AgendarCita(); break;
                    case "5": ConsultarPorPaciente(); break;
                    case "6": ConsultarPorMedico(); break;
                    case "7": ReprogramarCita(); break;
                    case "8": CancelarCita(); break;
                    case "9": salir = true; break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nERROR: Debes introducir un número o formato de fecha válido.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nERROR: {ex.Message}");
                Console.ResetColor();
            }

            if (!salir)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    private void RegistrarPaciente()
    {
        Console.Write("Nombre del paciente: ");
        string nombre = Console.ReadLine()!;
        Console.Write("Email: ");
        string email = Console.ReadLine()!;
        clinicaService.RegistrarPaciente(nombre, email);
        Console.WriteLine("Paciente registrado exitosamente.");
    }

    private void RegistrarEspecialidad()
    {
        Console.Write("Nombre de la especialidad: ");
        string nombre = Console.ReadLine()!;
        clinicaService.RegistrarEspecialidad(nombre);
        Console.WriteLine("Especialidad registrada.");
    }

    private void RegistrarMedico()
    {
        Console.WriteLine("\n--- Especialidades Disponibles ---");
        var especialidades = clinicaService.ObtenerEspecialidades();
        if (!especialidades.Any()) throw new Exception("Debe registrar una especialidad primero.");

        foreach (var e in especialidades) Console.WriteLine($"ID: {e.Id} - {e.Nombre}");

        Console.Write("\nNombre del médico: ");
        string nombre = Console.ReadLine()!;
        Console.Write("ID de la especialidad a asignar: ");
        int espId = int.Parse(Console.ReadLine()!);

        clinicaService.RegistrarMedico(nombre, espId);
        Console.WriteLine("Médico registrado y asignado a la especialidad.");
    }

    private void AgendarCita()
    {
        Console.WriteLine("\n--- Pacientes ---");
        var pacientes = clinicaService.ObtenerPacientes();
        if (!pacientes.Any()) throw new Exception("Debe registrar un paciente primero.");
        foreach (var p in pacientes) Console.WriteLine($"ID: {p.Id} - {p.Nombre}");
        Console.Write("ID del Paciente: ");
        int pacId = int.Parse(Console.ReadLine()!);

        Console.WriteLine("\n--- Médicos ---");
        var medicos = clinicaService.ObtenerMedicos();
        if (!medicos.Any()) throw new Exception("Debe registrar un médico primero.");
        foreach (var m in medicos) Console.WriteLine($"ID: {m.Id} - {m.Nombre} ({m.Especialidad?.Nombre})");
        Console.Write("ID del Médico: ");
        int medId = int.Parse(Console.ReadLine()!);

        Console.Write("Fecha y Hora (Ej. 2026-12-31 14:30): ");
        DateTime fecha = DateTime.Parse(Console.ReadLine()!);

        citaService.AgendarCita(pacId, medId, fecha);
    }

    private void ConsultarPorPaciente()
    {
        Console.Write("ID del Paciente: ");
        int id = int.Parse(Console.ReadLine()!);
        var citas = citaService.ConsultarPorPaciente(id);
        ImprimirCitas(citas);
    }

    private void ConsultarPorMedico()
    {
        Console.Write("ID del Médico: ");
        int id = int.Parse(Console.ReadLine()!);
        var citas = citaService.ConsultarPorMedico(id);
        ImprimirCitas(citas);
    }

    private void CancelarCita()
    {
        Console.Write("ID de la Cita a cancelar: ");
        int id = int.Parse(Console.ReadLine()!);
        citaService.CancelarCita(id);
    }

    private void ReprogramarCita()
    {
        Console.Write("ID de la Cita: ");
        int id = int.Parse(Console.ReadLine()!);
        Console.Write("Nueva Fecha y Hora (Ej. 2026-12-31 14:30): ");
        DateTime fecha = DateTime.Parse(Console.ReadLine()!);
        citaService.ReprogramarCita(id, fecha);
    }

    private void ImprimirCitas(IEnumerable<Cita> citas)
    {
        Console.WriteLine("\n--- Resultados ---");
        if (!citas.Any())
        {
            Console.WriteLine("No se encontraron citas.");
            return;
        }

        foreach (var c in citas)
        {
            string estado = c.EstaCancelada ? "[CANCELADA]" : "[ACTIVA]";
            Console.WriteLine($"Cita ID: {c.Id} | Paciente: {c.Paciente.Nombre} | Médico: {c.Medico.Nombre} | Fecha: {c.FechaHora:g} {estado}");
        }
    }
}