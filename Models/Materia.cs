using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Materia
    {
        public int MateriaId { get; set; }

        [Required]
        public string? NombreMateria { get; set; }

        [Required]
        public string? Docente { get; set; }

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}