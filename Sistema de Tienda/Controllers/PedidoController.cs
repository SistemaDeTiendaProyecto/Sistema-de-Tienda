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
    public class PedidoController : Controller
    {
        private readonly SistemaTiendaContext _context;

        public PedidoController(SistemaTiendaContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var sistemaTiendaContext = _context.Pedidos.Include(p => p.IdClienteNavigation);
            return View(await sistemaTiendaContext.ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
            return View(new Pedido());
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCliente,FechaPedido,Total,Estado,DetallesPedidos")] Pedido pedido)
        {

             Console.WriteLine($"Id: {pedido.Id}");
                Console.WriteLine($"IdCliente: {pedido.IdCliente}");
                Console.WriteLine($"FechaPedido: {pedido.FechaPedido}");
                Console.WriteLine($"Total: {pedido.Total}");
                Console.WriteLine($"Estado: {pedido.Estado}");
                Console.WriteLine($"DetallesPedido: {pedido.DetallesPedidos}");

                 if (pedido.DetallesPedidos != null )
    {
        Console.WriteLine("DetallesPedido:");
        foreach (var detalle in pedido.DetallesPedidos)
        {
            Console.WriteLine($"  Id: {detalle.Id}, ProductoId: {detalle.IdProducto}, Cantidad: {detalle.Cantidad}, Precio: {detalle.PrecioUnitario}"); // Asumiendo que DetallesPedido tiene estas propiedades
            // Agrega aquí las propiedades que quieras imprimir
        }
    }
    else
    {
        Console.WriteLine("DetallesPedido: Lista vacía o nula");
    }
           
                _context.Add(pedido);
                await _context.SaveChangesAsync();
              
            
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pedido.IdCliente);
            return View(pedido);
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pedido.IdCliente);
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCliente,FechaPedido,Total,Estado")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pedido.IdCliente);
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
