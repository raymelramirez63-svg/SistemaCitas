namespace SistemaCitas.Utils;

public static class Validador
{
    public static void ValidarTexto(string texto, string nombreCampo)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException($"El campo '{nombreCampo}' no puede estar vacio.");
    }
}