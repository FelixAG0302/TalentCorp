using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

[Authorize]
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
    [Authorize(Roles = "ADMIN")]
    public IActionResult Create()
    {
        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Nombre");
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre");
        return View();
    }

    // POST: Entrevistas/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CandidatoId,PuestoId,FechaEntrevista")] Entrevista entrevista)
    {
        context.Add(entrevista);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Entrevistas/Edit/5
    [Authorize(Roles = "ADMIN")]
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

        ViewData["CandidatoId"] = new SelectList(context.Candidatos, "Id", "Nombre", entrevista.CandidatoId);
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre", entrevista.PuestoId);
        return View(entrevista);
    }

    // POST: Entrevistas/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,CandidatoId,PuestoId,FechaEntrevista")]
        Entrevista entrevista)
    {
        if (id != entrevista.Id)
        {
            return NotFound();
        }

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

    // GET: Entrevistas/Delete/5
    [Authorize(Roles = "ADMIN")]
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
    [Authorize(Roles = "ADMIN")]
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