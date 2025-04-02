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
    public class DetallesPedidoController : Controller
    {
        private readonly SistemaTiendaContext _context;

        public DetallesPedidoController(SistemaTiendaContext context)
        {
            _context = context;
        }

        // GET: DetallesPedido
        public async Task<IActionResult> Index()
        {
            var sistemaTiendaContext = _context.DetallesPedidos.Include(d => d.IdPedidoNavigation).Include(d => d.IdProductoNavigation);
            return View(await sistemaTiendaContext.ToListAsync());
        }

        // GET: DetallesPedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesPedido = await _context.DetallesPedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesPedido == null)
            {
                return NotFound();
            }

            return View(detallesPedido);
        }

        // GET: DetallesPedido/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id");
            return View();
        }

        // POST: DetallesPedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPedido,IdProducto,Item,Cantidad,PrecioUnitario,Subtotal")] DetallesPedido detallesPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallesPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallesPedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallesPedido.IdProducto);
            return View(detallesPedido);
        }

        // GET: DetallesPedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesPedido = await _context.DetallesPedidos.FindAsync(id);
            if (detallesPedido == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallesPedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallesPedido.IdProducto);
            return View(detallesPedido);
        }

        // POST: DetallesPedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPedido,IdProducto,Item,Cantidad,PrecioUnitario,Subtotal")] DetallesPedido detallesPedido)
        {
            if (id != detallesPedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallesPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesPedidoExists(detallesPedido.Id))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallesPedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallesPedido.IdProducto);
            return View(detallesPedido);
        }

        // GET: DetallesPedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesPedido = await _context.DetallesPedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesPedido == null)
            {
                return NotFound();
            }

            return View(detallesPedido);
        }

        // POST: DetallesPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallesPedido = await _context.DetallesPedidos.FindAsync(id);
            if (detallesPedido != null)
            {
                _context.DetallesPedidos.Remove(detallesPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesPedidoExists(int id)
        {
            return _context.DetallesPedidos.Any(e => e.Id == id);
        }
    }
}
