using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

public class EducacionesController(TalentCorpContext context) : Controller
{
    // GET: Educaciones
    public async Task<IActionResult> Index()
    {
        var talentCorpContext = context.Educacions.Include(e => e.Candidato);
        return View(await talentCorpContext.ToListAsync());
    }

    // GET: Educaciones/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var educacion = await context.Educacions
            .Include(e => e.Candidato)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (educacion == null)
        {
            return NotFound();
        }

        return View(educacion);
    }

    // GET: Educaciones/Create
    public IActionResult Create()
    {
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id");
        return View();
    }

    // POST: Educaciones/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CandidatoId,Nivel,Institucion,Idiomas,FechaDesde,FechaHasta")] Educacion educacion)
    {
        if (ModelState.IsValid)
        {
            context.Add(educacion);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", educacion.CandidatoId);
        return View(educacion);
    }

    // GET: Educaciones/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var educacion = await context.Educacions.FindAsync(id);
        if (educacion == null)
        {
            return NotFound();
        }
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", educacion.CandidatoId);
        return View(educacion);
    }

    // POST: Educaciones/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,CandidatoId,Nivel,Institucion,Idiomas,FechaDesde,FechaHasta")] Educacion educacion)
    {
        if (id != educacion.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(educacion);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducacionExists(educacion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", educacion.CandidatoId);
        return View(educacion);
    }

    // GET: Educaciones/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var educacion = await context.Educacions
            .Include(e => e.Candidato)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (educacion == null)
        {
            return NotFound();
        }

        return View(educacion);
    }

    // POST: Educaciones/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var educacion = await context.Educacions.FindAsync(id);
        if (educacion != null)
        {
            context.Educacions.Remove(educacion);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EducacionExists(int id)
    {
        return context.Educacions.Any(e => e.Id == id);
    }
}