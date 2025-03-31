using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Tienda.Models;

namespace Sistema_de_Tienda.Controllers
{
    public class TiendaController : Controller
    {
        private readonly SistemaTiendaContext _context;

        public TiendaController(SistemaTiendaContext context)
        {
            _context = context;
        }

        // GET: Tienda

        public async Task<IActionResult> Index(Tienda tienda, int topRegistro = 10)
        {
            var query = _context.Tiendas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(tienda.Nombre))
                query = query.Where(s => s.Nombre.Contains(tienda.Nombre));
            if (!string.IsNullOrWhiteSpace(tienda.Email))
                query = query.Where(s => s.Email.Contains(tienda.Email));
            if (!string.IsNullOrWhiteSpace(tienda.Direccion))
                query = query.Where(s => s.Direccion.Contains(tienda.Direccion));
            if (tienda.Id > 0)
                query = query.Where(s => s.Id == tienda.Id);
            if (tienda.Id > 0)
                query = query.Where(s => s.Id == tienda.Id);
            if (topRegistro > 0)
                query = query.Take(topRegistro);

            return View(await query.ToListAsync());
        }

        // GET: Tienda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tienda = await _context.Tiendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tienda == null)
            {
                return NotFound();
            }

            return View(tienda);
        }

        // GET: Tienda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tienda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,Direccion")] Tienda tienda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tienda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tienda);
        }

        // GET: Tienda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }
            return View(tienda);
        }

        // POST: Tienda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email,Direccion")] Tienda tienda)
        {
            if (id != tienda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tienda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiendaExists(tienda.Id))
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
            return View(tienda);
        }

        // GET: Tienda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tienda = await _context.Tiendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tienda == null)
            {
                return NotFound();
            }

            return View(tienda);
        }

        // POST: Tienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda != null)
            {
                _context.Tiendas.Remove(tienda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiendaExists(int id)
        {
            return _context.Tiendas.Any(e => e.Id == id);
        }
    }
}
