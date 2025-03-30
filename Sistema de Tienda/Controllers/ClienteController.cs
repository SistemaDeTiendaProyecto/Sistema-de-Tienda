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
    public class ClienteController : Controller
    {
        private readonly SistemaTiendaContext _context;

        public ClienteController(SistemaTiendaContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index(Cliente cliente, int topRegistro = 10)
        {
            var query = _context.Clientes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(cliente.Nombre))
                query = query.Where(s => s.Nombre.Contains(cliente.Nombre));
            if (!string.IsNullOrWhiteSpace(cliente.Correo))
                query = query.Where(s => s.Correo.Contains(cliente.Correo));
            if (!string.IsNullOrWhiteSpace(cliente.Dui))
                query = query.Where(s => s.Dui.Contains(cliente.Dui));
            if (cliente.Id > 0)
                query = query.Where(s => s.Id == cliente.Id);
            if (cliente.Id > 0)
                query = query.Where(s => s.Id == cliente.Id);
            if (topRegistro > 0)
                query = query.Take(topRegistro);

            return View(await query.ToListAsync());
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Correo,Contrasena,DireccionPrincipal,Dui,FechaNacimiento,Telefono,FechaRegistro")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Correo,DireccionPrincipal,Telefono,Id")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }
            var usuarioUpdate = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == cliente.Id);

            try
            {
                usuarioUpdate.Nombre = cliente.Nombre;
                usuarioUpdate.Correo = cliente.Correo;
                usuarioUpdate.DireccionPrincipal = cliente.DireccionPrincipal;
                usuarioUpdate.Telefono = cliente.Telefono;
                _context.Update(usuarioUpdate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(cliente);
                }
            }
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
