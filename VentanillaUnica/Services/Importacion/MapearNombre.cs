namespace VentanillaUnica.Services.Importacion;

public partial class ImportacionService
{
    private void MapearNombreCompleto(string nombreCompleto, Models.Ciudadano ciudadano)
    {
        // Dividir por espacios
        string[] partes = nombreCompleto.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (partes.Length == 2)
        {
            // Ejemplo: "MARCELINO ARCOS"
            ciudadano.PrimerNombre = partes[0];
            ciudadano.PrimerApellido = partes[1];
        }
        else if (partes.Length == 3)
        {
            ciudadano.PrimerNombre = partes[0];
            ciudadano.SegundoNombre = partes[1];
            ciudadano.PrimerApellido = partes[2];
        }
        else if (partes.Length >= 4)
        {
            ciudadano.PrimerNombre = partes[0];
            ciudadano.SegundoNombre = partes[1];
            ciudadano.PrimerApellido = partes[2];
            ciudadano.SegundoApellido = partes[3];
            
            if (partes.Length > 4)
            {
                ciudadano.SegundoApellido = string.Join(" ", partes.Skip(3));
            }
        }
        else if (partes.Length == 1)
        {
            ciudadano.PrimerNombre = partes[0];
            ciudadano.PrimerApellido = "REVISAR";
        }
    }
}