using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Alumno
    {
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string? Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo Grado es obligatorio.")]
        public string? Grado { get; set; }

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}