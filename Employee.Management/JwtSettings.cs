namespace Employees.Management
{
    public class JwtSettings
    {
        //Propiedades de la clase
        //validar si es usuario
        public bool ValidateIssuerSigningKey { get; set; }//comprobar firma del usuario
        public string IssuerSigningKey { get; set; } = string.Empty;//comprobar esta firma
        //Usuario
        public bool ValidateIssuer { get; set; } = true;//validar usuario
        public string? ValidIssuer { get; set; }
        //Audiencia
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }
        //Tiempo de vida del JWT
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifeTime { get; set; } = true;
    }
}
