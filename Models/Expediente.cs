using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Expediente
    {
        public int ExpedienteId { get; set; }

        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }

        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }

        public float NotaFinal { get; set; }

        public string? Observaciones { get; set; }
    }
}