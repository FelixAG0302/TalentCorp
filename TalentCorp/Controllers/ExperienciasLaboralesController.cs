using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

public class ExperienciasLaboralesController(TalentCorpContext context) : Controller
{
    // GET: ExperienciasLaborales
    public async Task<IActionResult> Index()
    {
        var talentCorpContext = context.ExperienciaLaborals.Include(e => e.Candidato);
        return View(await talentCorpContext.ToListAsync());
    }

    // GET: ExperienciasLaborales/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var experienciaLaboral = await context.ExperienciaLaborals
            .Include(e => e.Candidato)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (experienciaLaboral == null)
        {
            return NotFound();
        }

        return View(experienciaLaboral);
    }

    // GET: ExperienciasLaborales/Create
    public IActionResult Create()
    {
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id");
        return View();
    }

    // POST: ExperienciasLaborales/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CandidatoId,Empresa,PuestoOcupado,FechaDesde,FechaHasta,Salario")] ExperienciaLaboral experienciaLaboral)
    {
        if (ModelState.IsValid)
        {
            context.Add(experienciaLaboral);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", experienciaLaboral.CandidatoId);
        return View(experienciaLaboral);
    }

    // GET: ExperienciasLaborales/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var experienciaLaboral = await context.ExperienciaLaborals.FindAsync(id);
        if (experienciaLaboral == null)
        {
            return NotFound();
        }
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", experienciaLaboral.CandidatoId);
        return View(experienciaLaboral);
    }

    // POST: ExperienciasLaborales/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,CandidatoId,Empresa,PuestoOcupado,FechaDesde,FechaHasta,Salario")] ExperienciaLaboral experienciaLaboral)
    {
        if (id != experienciaLaboral.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(experienciaLaboral);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienciaLaboralExists(experienciaLaboral.Id))
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
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", experienciaLaboral.CandidatoId);
        return View(experienciaLaboral);
    }

    // GET: ExperienciasLaborales/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var experienciaLaboral = await context.ExperienciaLaborals
            .Include(e => e.Candidato)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (experienciaLaboral == null)
        {
            return NotFound();
        }

        return View(experienciaLaboral);
    }

    // POST: ExperienciasLaborales/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var experienciaLaboral = await context.ExperienciaLaborals.FindAsync(id);
        if (experienciaLaboral != null)
        {
            context.ExperienciaLaborals.Remove(experienciaLaboral);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ExperienciaLaboralExists(int id)
    {
        return context.ExperienciaLaborals.Any(e => e.Id == id);
    }
}