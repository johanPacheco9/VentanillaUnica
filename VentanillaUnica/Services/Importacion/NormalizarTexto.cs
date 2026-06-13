using System.Text.RegularExpressions;
namespace VentanillaUnica.Services.Importacion;

public partial class ImportacionService
{
    private string NormalizarTexto(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return string.Empty;
        string resultado = texto.Replace("\r", "").Replace("\n", " ").Trim();
        resultado = Regex.Replace(resultado, @"\s+", " ");
        return resultado.ToUpper();
    }
}