using System;
using System.Collections.Generic;
using System.Text;
namespace SistemaCitas.Interfaces;

public interface INotificador
{
    void EnviarMensaje(string destinatario, string mensaje);
}