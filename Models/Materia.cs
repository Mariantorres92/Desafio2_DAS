using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Materia
    {
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "El campo Nombre de Materia es obligatorio.")]
        public string? NombreMateria { get; set; }

        [Required(ErrorMessage = "El campo Docente es obligatorio.")]
        public string? Docente { get; set; }

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}