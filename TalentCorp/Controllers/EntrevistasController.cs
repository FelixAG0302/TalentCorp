using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

public class EntrevistasController(TalentCorpContext context) : Controller
{
    // GET: Entrevistas
    public async Task<IActionResult> Index()
    {
        var talentCorpContext = context.Entrevistas.Include(e => e.Candidato).Include(e => e.Puesto);
        return View(await talentCorpContext.ToListAsync());
    }

    // GET: Entrevistas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entrevista = await context.Entrevistas
            .Include(e => e.Candidato)
            .Include(e => e.Puesto)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (entrevista == null)
        {
            return NotFound();
        }

        return View(entrevista);
    }

    // GET: Entrevistas/Create
    public IActionResult Create()
    {
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id");
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Id");
        return View();
    }

    // POST: Entrevistas/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CandidatoId,PuestoId,FechaEntrevista")] Entrevista entrevista)
    {
        if (ModelState.IsValid)
        {
            context.Add(entrevista);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", entrevista.CandidatoId);
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Id", entrevista.PuestoId);
        return View(entrevista);
    }

    // GET: Entrevistas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entrevista = await context.Entrevistas.FindAsync(id);
        if (entrevista == null)
        {
            return NotFound();
        }

        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", entrevista.CandidatoId);
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Id", entrevista.PuestoId);
        return View(entrevista);
    }

    // POST: Entrevistas/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,CandidatoId,PuestoId,FechaEntrevista")] Entrevista entrevista)
    {
        if (id != entrevista.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(entrevista);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrevistaExists(entrevista.Id))
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

        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Id", entrevista.CandidatoId);
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Id", entrevista.PuestoId);
        return View(entrevista);
    }

    // GET: Entrevistas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entrevista = await context.Entrevistas
            .Include(e => e.Candidato)
            .Include(e => e.Puesto)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (entrevista == null)
        {
            return NotFound();
        }

        return View(entrevista);
    }

    // POST: Entrevistas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entrevista = await context.Entrevistas.FindAsync(id);
        if (entrevista != null)
        {
            context.Entrevistas.Remove(entrevista);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EntrevistaExists(int id)
    {
        return context.Entrevistas.Any(e => e.Id == id);
    }
}