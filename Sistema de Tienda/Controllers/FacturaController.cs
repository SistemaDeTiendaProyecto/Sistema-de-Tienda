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
    public class FacturaController : Controller
    {
        private readonly SistemaTiendaContext _context;

        public FacturaController(SistemaTiendaContext context)
        {
            _context = context;
        }

        // GET: Factura
        public async Task<IActionResult> Index()
        {
            var sistemaTiendaContext = _context.Facturas.Include(f => f.IdClienteNavigation).Include(f => f.IdPagoNavigation).Include(f => f.IdPedidoNavigation);
            return View(await sistemaTiendaContext.ToListAsync());
        }

        // GET: Factura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdPagoNavigation)
                .Include(f => f.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Factura/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Contrasena");
            ViewData["IdPago"] = new SelectList(_context.Pagos, "Id", "Id");
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id");
            return View();
        }

        // POST: Factura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPedido,IdPago,IdCliente,FechaFacturacion,TipoFacturacion,MetodoEnvio,Total")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Contrasena", factura.IdCliente);
            ViewData["IdPago"] = new SelectList(_context.Pagos, "Id", "Id", factura.IdPago);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", factura.IdPedido);
            return View(factura);
        }

        // GET: Factura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Contrasena", factura.IdCliente);
            ViewData["IdPago"] = new SelectList(_context.Pagos, "Id", "Id", factura.IdPago);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", factura.IdPedido);
            return View(factura);
        }

        // POST: Factura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPedido,IdPago,IdCliente,FechaFacturacion,TipoFacturacion,MetodoEnvio,Total")] Factura factura)
        {
            if (id != factura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Contrasena", factura.IdCliente);
            ViewData["IdPago"] = new SelectList(_context.Pagos, "Id", "Id", factura.IdPago);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", factura.IdPedido);
            return View(factura);
        }

        // GET: Factura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdPagoNavigation)
                .Include(f => f.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}
