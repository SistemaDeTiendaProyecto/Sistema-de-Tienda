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
    [Authorize(Roles = "ADMINISTRADOR, GERENTE")]


    public class ProductosController : Controller
    {
        private readonly SistemaTiendaContext _context;

        public ProductosController(SistemaTiendaContext context)
        {
            _context = context;
        }

            public async Task<byte[]?> GenerarByteImage(IFormFile? file, byte[]?  bytesImage =null)
        {
            byte[]? bytes = bytesImage;
            if (file != null && file.Length > 0)
            {
                // Construir la ruta del archivo               
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    bytes= memoryStream.ToArray(); // Devuelve los bytes del archivo
                }
            }
            return bytes;
        }



        // GET: Productos
        public async Task<IActionResult> Index(string Nombre, string Categoria, string Tienda)
        {
            var query = _context.Productos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdTiendaNavigation).AsQueryable();

            // Filtrar por Nombre
            if (!string.IsNullOrWhiteSpace(Nombre))
            {
                query = query.Where(p => p.Nombre.Contains(Nombre));
            }

            // Filtrar por Categoría
            if (!string.IsNullOrWhiteSpace(Categoria))
            {
                query = query.Where(p => p.IdCategoriaNavigation.Nombre.Contains(Categoria));
            }

            // Filtrar por Tienda
            if (!string.IsNullOrWhiteSpace(Tienda))
            {
                query = query.Where(p => p.IdTiendaNavigation.Nombre.Contains(Tienda));
            }

            return View(await query.ToListAsync());
        }


        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdTiendaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["IdTienda"] = new SelectList(_context.Tiendas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        
        public async Task<IActionResult> Create([Bind("IdCategoria,IdTienda,Nombre,Image,Descripcion,Precio,Stock,Activo")] Producto producto, IFormFile? file = null)
        {
             Console.WriteLine($"IdCategoria: {producto.IdCategoria}");
                Console.WriteLine($"IdTienda: {producto.IdTienda}");
                Console.WriteLine($"Nombre: {producto.Nombre}");
                Console.WriteLine($"Image: {producto.Image}");
      
                Console.WriteLine($"Image: {file}");
                Console.WriteLine($"Descripcion: {producto.Descripcion}");
                Console.WriteLine($"Precio: {producto.Precio}");
                Console.WriteLine($"Stock: {producto.Stock}");
                Console.WriteLine($"Activo: {producto.Activo}");

                 producto.IdCategoria = producto.IdCategoria;
                    producto.IdTienda = producto.IdTienda;
                    producto.Nombre = producto.Nombre;
                    producto.Descripcion = producto.Descripcion;
                    producto.Precio = producto.Precio;
                    producto.Stock = producto.Stock;
                    producto.Activo = producto.Activo;

                

                 producto.Image = await GenerarByteImage(file);
                 Console.WriteLine($"Image: {producto.Image}");
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

            // ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", producto.IdCategoria);
            // ViewData["IdTienda"] = new SelectList(_context.Tiendas, "Id", "Nombre", producto.IdTienda);
            // return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id", producto.IdCategoria);
            ViewData["IdTienda"] = new SelectList(_context.Tiendas, "Id", "Id", producto.IdTienda);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCategoria,IdTienda,Nombre,Image,Descripcion,Precio,Stock,Activo")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id", producto.IdCategoria);
            ViewData["IdTienda"] = new SelectList(_context.Tiendas, "Id", "Id", producto.IdTienda);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdTiendaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
