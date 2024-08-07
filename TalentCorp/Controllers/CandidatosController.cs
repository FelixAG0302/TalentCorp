using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentCorp.Context;
using TalentCorp.Entities;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;


namespace TalentCorp.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly TalentCorpContext context;

        public CandidatosController(TalentCorpContext context)
        {
            this.context = context;
        }

        // GET: Candidatos
        public async Task<IActionResult> Index(string sortOrder, string selectedPuesto)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["ApellidoSortParm"] = sortOrder == "Apellido" ? "apellido_desc" : "Apellido";
            ViewData["CurrentFilter"] = selectedPuesto;

            // Obtener la lista de puestos y añadir la opción "Todos los puestos"
            var puestos = await context.Puestos.Select(p => p.Nombre).Distinct().ToListAsync();
            puestos.Insert(0, "Todos los puestos");

            // Codificar los datos JSON
            var jsonPuestos = JsonConvert.SerializeObject(puestos);
            ViewData["Puestos"] = jsonPuestos;

            var candidatos = from c in context.Candidatos.Include(c => c.Puesto)
                             select c;

            if (!string.IsNullOrEmpty(selectedPuesto) && selectedPuesto != "Todos los puestos")
            {
                candidatos = candidatos.Where(c => c.Puesto.Nombre == selectedPuesto);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    candidatos = candidatos.OrderByDescending(c => c.Nombre);
                    break;
                case "Date":
                    candidatos = candidatos.OrderBy(c => c.FechaIngreso);
                    break;
                case "date_desc":
                    candidatos = candidatos.OrderByDescending(c => c.FechaIngreso);
                    break;
                case "Apellido":
                    candidatos = candidatos.OrderBy(c => c.Apellido);
                    break;
                case "apellido_desc":
                    candidatos = candidatos.OrderByDescending(c => c.Apellido);
                    break;
                default:
                    candidatos = candidatos.OrderBy(c => c.Nombre);
                    break;
            }

            return View(await candidatos.ToListAsync());
        }


        public async Task<IActionResult> Export()
        {
            var candidatos = await context.Candidatos
                .ToListAsync();

            var table = ConvertToDataTable(candidatos);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                for (var i = 0; i < table.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = table.Columns[i].ColumnName;
                }

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    for (var j = 0; j < table.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = table.Rows[i][j].ToString();
                    }
                }

                FileInfo? file = null;
                try
                {
                    file = new FileInfo("candidatos.xlsx");
                }
                catch (Exception)
                {
                    Console.WriteLine("Ha ocurrido un error al intentar exportar la información de los Candidatos.");
                }

                if (file != null)
                {
                    await excelPackage.SaveAsAsync(file);
                }
            }

            Console.WriteLine("Datos exportados a Excel exitosamente.");

            return RedirectToAction(nameof(Index));
        }

        private static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            var properties = typeof(T).GetProperties();
            var dataTable = new DataTable();

            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
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
                candidato.Puesto
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
        public async Task<IActionResult> Create([Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,PuestoId,Departamento")] Candidato candidato)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,PuestoId,Departamento")] Candidato candidato)
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
}
