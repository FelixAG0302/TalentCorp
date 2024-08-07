using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

public class CandidatosController(TalentCorpContext context) : Controller
{
    // GET: Candidatos
    public async Task<IActionResult> Index()
    {
        var talentCorpContext = context.Candidatos.Include(c => c.Puesto);
        return View(await talentCorpContext.ToListAsync());
    }

    // GET: Candidatos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidato = await context.Candidatos
            .Include(c => c.Puesto)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (candidato == null)
        {
            return NotFound();
        }

        return View(candidato);
    }

    public async Task<IActionResult> MakeEmployee(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidato = await context.Candidatos
            .Include(m => m.Puesto)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (candidato == null)
        {
            return NotFound();
        }

        var empleado = new Empleado(
            candidato.Cédula,
            candidato.Nombre,
            candidato.Apellido,
            candidato.FechaIngreso,
            "ACTIVO",
            candidato.PuestoId,
            candidato.Puesto!
        );

        await context.Empleados.AddAsync(empleado);
        context.Candidatos.Remove(candidato);

        await context.SaveChangesAsync();

        return RedirectToAction("Index", "Empleados");
    }

    // GET: Candidatos/Create
    public IActionResult Create()
    {
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre");
        return View();
    }

    // POST: Candidatos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,PuestoId,Departamento")]
        Candidato candidato)
    {
        if (ModelState.IsValid)
        {
            context.Add(candidato);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre", candidato.PuestoId);
        return View(candidato);
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

        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre", candidato.PuestoId);
        return View(candidato);
    }

    // POST: Candidatos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,PuestoId,Departamento")]
        Candidato candidato)
    {
        if (id != candidato.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
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

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre", candidato.PuestoId);
        return View(candidato);
    }

    // GET: Candidatos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidato = await context.Candidatos
            .Include(c => c.Puesto)
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