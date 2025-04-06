using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Tienda.Models;

namespace Sistema_de_Tienda.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR, CLIENTE")]
    public class PagosController : Controller
    {
        private readonly SistemaTiendaContext _context;

        public PagosController(SistemaTiendaContext context)
        {
            _context = context;
        }

        // GET: Pagos
        public async Task<IActionResult> Index(string MetodoPago, string Estado, string Cliente)
        {
            var query = _context.Pagos.Include(p => p.IdClienteNavigation).Include(p => p.IdPedidoNavigation).AsQueryable();

            // Filtrar por Método de Pago
            if (!string.IsNullOrWhiteSpace(MetodoPago))
            {
                query = query.Where(p => p.MetodoPago.Contains(MetodoPago));
            }

            // Filtrar por Estado
            if (!string.IsNullOrWhiteSpace(Estado))
            {
                query = query.Where(p => p.Estado.Contains(Estado));
            }

            // Filtrar por Cliente
            if (!string.IsNullOrWhiteSpace(Cliente))
            {
                query = query.Where(p => p.IdClienteNavigation.Nombre.Contains(Cliente));
            }

            return View(await query.ToListAsync());
        }
        public async Task<IActionResult> MetodoPago()
        {
            return View();
        }
        // GET: Pagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // GET: Pagos/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewBag.IdCliente = new SelectList(_context.Clientes.Where(c => c.Role == "CLIENTE"), "Id", "Nombre");
            return View();
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPedido,IdCliente,FechaPago,MetodoPago,Estado")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IdCliente = new SelectList(_context.Clientes.Where(c => c.Role == "CLIENTE"), "Id", "Nombre", pago.IdCliente);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", pago.IdPedido);
            return View(pago);
        }

        // GET: Pagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pago.IdCliente);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", pago.IdPedido);
            return View(pago);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPedido,IdCliente,FechaPago,MetodoPago,Estado")] Pago pago)
        {
            if (id != pago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pago.IdCliente);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", pago.IdPedido);
            return View(pago);
        }

        // GET: Pagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.Id == id);
        }
    }
}
