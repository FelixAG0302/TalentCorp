using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TalentCorp.Entities;

namespace TalentCorp.Controllers
{
    public class CandidatoesController(TalentCorpContext context) : Controller
    {
        // GET: Candidatoes
        public async Task<IActionResult> Index()
        {
            return View(await context.Candidatos.ToListAsync());
        }

        // GET: Candidatoes/Details/5
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

        // GET: Candidatoes/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Export()
        {
            var candidatos = await context.Candidatos
                .ToListAsync();

            var table = ConvertToDataTable(candidatos);

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

        // POST: Candidatoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,Departamento,Estado")]
            Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                return View(candidato);
            }

            context.Add(candidato);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Candidatoes/Edit/5
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

        // POST: Candidatoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Cédula,Nombre,Apellido,FechaIngreso,Departamento,Estado")]
            Candidato candidato)
        {
            if (id != candidato.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(candidato);
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

        // GET: Candidatoes/Delete/5
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

        // POST: Candidatoes/Delete/5
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