namespace SistemaCitas.Services;

using SistemaCitas.Interfaces;

public class EmailNotificador : INotificador
{
    public void EnviarMensaje(string destinatario, string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n[EMAIL ENVIADO A: {destinatario}] -> {mensaje}");
        Console.ResetColor();
    }
}