using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

[Authorize]
public class PuestosController(TalentCorpContext context) : Controller
{
    // GET: Puestos
    public async Task<IActionResult> Index()
    {
        return View(await context.Puestos.ToListAsync());
    }

    // GET: Puestos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var puesto = await context.Puestos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (puesto == null)
        {
            return NotFound();
        }

        return View(puesto);
    }

    // GET: Puestos/Create
    [Authorize(Roles = "ADMIN")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Puestos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Nombre,Descripcion,NivelRiesgo,SalarioMin,SalarioMax,Estado")] Puesto puesto)
    {
        context.Add(puesto);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Puestos/Edit/5
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var puesto = await context.Puestos.FindAsync(id);
        if (puesto == null)
        {
            return NotFound();
        }

        return View(puesto);
    }

    // POST: Puestos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Nombre,Descripcion,NivelRiesgo,SalarioMin,SalarioMax,Estado")] Puesto puesto)
    {
        if (id != puesto.Id)
        {
            return NotFound();
        }

        try
        {
            context.Update(puesto);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PuestoExists(puesto.Id))
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

    // GET: Puestos/Delete/5
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var puesto = await context.Puestos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (puesto == null)
        {
            return NotFound();
        }

        return View(puesto);
    }

    // POST: Puestos/Delete/5
    [Authorize(Roles = "ADMIN")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var puesto = await context.Puestos.FindAsync(id);
        if (puesto != null)
        {
            context.Puestos.Remove(puesto);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PuestoExists(int id)
    {
        return context.Puestos.Any(e => e.Id == id);
    }
}