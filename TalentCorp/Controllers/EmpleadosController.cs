using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;

namespace TalentCorp.Controllers;

public class EmpleadosController(TalentCorpContext context) : Controller
{
    // GET: Empleados
    public async Task<IActionResult> Index()
    {
        var talentCorpContext = context.Empleados.Include(e => e.Puesto);
        return View(await talentCorpContext.ToListAsync());
    }

    // GET: Empleados/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empleado = await context.Empleados
            .Include(e => e.Puesto)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empleado == null)
        {
            return NotFound();
        }

        return View(empleado);
    }

    // GET: Empleados/Create
    public IActionResult Create()
    {
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre");
        return View();
    }

    // POST: Empleados/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Cedula,Nombre,Apellido,FechaIngreso,Estado,PuestoId")] Empleado empleado)
    {
        if (ModelState.IsValid)
        {
            context.Add(empleado);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre", empleado.PuestoId);
        return View(empleado);
    }

    // GET: Empleados/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empleado = await context.Empleados.FindAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre", empleado.PuestoId);
        return View(empleado);
    }

    // POST: Empleados/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,Nombre,Apellido,FechaIngreso,Estado,PuestoId")] Empleado empleado)
    {
        if (id != empleado.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(empleado);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(empleado.Id))
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
        ViewData["PuestoId"] = new SelectList(context.Puestos, "Id", "Nombre", empleado.PuestoId);
        return View(empleado);
    }

    // GET: Empleados/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var empleado = await context.Empleados
            .Include(e => e.Puesto)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (empleado == null)
        {
            return NotFound();
        }

        return View(empleado);
    }

    // POST: Empleados/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var empleado = await context.Empleados.FindAsync(id);
        if (empleado != null)
        {
            context.Empleados.Remove(empleado);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EmpleadoExists(int id)
    {
        return context.Empleados.Any(e => e.Id == id);
    }
}