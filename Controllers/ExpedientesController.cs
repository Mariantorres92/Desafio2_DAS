using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColegioSanJose.Data;
using ColegioSanJose.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ColegioSanJose.Controllers
{
    public class ExpedientesController : Controller
    {
        private readonly AppDbContext _context;

        public ExpedientesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var expedientes = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .ToListAsync();
            return View(expedientes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expediente == null) return NotFound();
            return View(expediente);
        }

        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre");
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expediente expediente)
        {
            if (string.IsNullOrEmpty(expediente.Observaciones))
                expediente.Observaciones = "-";

            ModelState.Remove("Alumno");
            ModelState.Remove("Materia");

            if (expediente.AlumnoId == 0)
                ModelState.AddModelError("AlumnoId", "Debe seleccionar un Alumno.");

            if (expediente.MateriaId == 0)
                ModelState.AddModelError("MateriaId", "Debe seleccionar una Materia.");

            if (ModelState.IsValid)
            {
                _context.Add(expediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var expediente = await _context.Expedientes.FindAsync(id);
            if (expediente == null) return NotFound();
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expediente expediente)
        {
            if (id != expediente.ExpedienteId) return NotFound();

            if (string.IsNullOrEmpty(expediente.Observaciones))
                expediente.Observaciones = "-";

            ModelState.Remove("Alumno");
            ModelState.Remove("Materia");

            if (expediente.AlumnoId == 0)
                ModelState.AddModelError("AlumnoId", "Debe seleccionar un Alumno.");

            if (expediente.MateriaId == 0)
                ModelState.AddModelError("MateriaId", "Debe seleccionar una Materia.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Expedientes.Any(e => e.ExpedienteId == expediente.ExpedienteId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "Nombre", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expediente == null) return NotFound();
            return View(expediente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expediente = await _context.Expedientes.FindAsync(id);
            if (expediente != null) _context.Expedientes.Remove(expediente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Promedios()
        {
            var promedios = await _context.Expedientes
                .Include(e => e.Alumno)
                .GroupBy(e => new { e.AlumnoId, e.Alumno!.Nombre, e.Alumno.Apellido })
                .Select(g => new
                {
                    Alumno = g.Key.Nombre + " " + g.Key.Apellido,
                    Promedio = g.Average(e => e.NotaFinal),
                    TotalMaterias = g.Count()
                })
                .ToListAsync();

            ViewBag.Promedios = promedios;
            return View();
        }
    }
}