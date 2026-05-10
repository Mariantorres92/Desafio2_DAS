using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Expediente
    {
        public int ExpedienteId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Alumno.")]
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una Materia.")]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }

        [Required(ErrorMessage = "El campo Nota Final es obligatorio.")]
        public float NotaFinal { get; set; }

        public string? Observaciones { get; set; }
    }
}