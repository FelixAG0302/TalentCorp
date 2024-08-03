using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

[Authorize]
public class CandidatosController(TalentCorpContext context) : Controller
{
    // GET: Candidatos
    public async Task<IActionResult> Index()
    {
        return View(await context.Candidatos.ToListAsync());
    }

    // GET: Candidatos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidato = await context.Candidatos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (candidato == null)
        {
            return NotFound();
        }

        return View(candidato);
    }

    // GET: Candidatos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Candidatos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,Departamento")]
        Candidato candidato)
    {
        context.Add(candidato);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Candidatos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidato = await context.Candidatos.FindAsync(id);
        if (candidato == null)
        {
            return NotFound();
        }

        return View(candidato);
    }

    // POST: Candidatos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,Departamento")]
        Candidato candidato)
    {
        if (id != candidato.Id)
        {
            return NotFound();
        }

        try
        {
            context.Update(candidato);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CandidatoExists(candidato.Id))
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

    // GET: Candidatos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidato = await context.Candidatos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (candidato == null)
        {
            return NotFound();
        }

        return View(candidato);
    }

    // POST: Candidatos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var candidato = await context.Candidatos.FindAsync(id);
        if (candidato != null)
        {
            context.Candidatos.Remove(candidato);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CandidatoExists(int id)
    {
        return context.Candidatos.Any(e => e.Id == id);
    }
}