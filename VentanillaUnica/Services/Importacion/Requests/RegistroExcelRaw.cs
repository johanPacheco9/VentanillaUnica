namespace VentanillaUnica.Services.Importacion.Requests;

public class RegistroExcelRaw
{
    public string Datos { get; set; } = string.Empty;                // Nombre completo del ciudadano
    public string NumeroIdentificacion { get; set; } = string.Empty; // Cédula o texto temporal (ej: "PLACA 012")
    public string Fecha { get; set; } = string.Empty;                // Fecha del radicado en texto
    public string NoRadicado { get; set; } = string.Empty;           // Consecutivo del radicado
    public string NoFolios { get; set; } = string.Empty;             // Cantidad de hojas
    public string Placa { get; set; } = string.Empty;                // Placa del vehículo si aplica
    public string Asunto { get; set; } = string.Empty;               // Descripción / Motivo del trámite
    public string Responsable { get; set; } = string.Empty;          // Quién atiende (ej: DIANA, JOHANA)
}