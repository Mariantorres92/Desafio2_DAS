using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Alumno
    {
        public int AlumnoId { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string? Grado { get; set; }

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}