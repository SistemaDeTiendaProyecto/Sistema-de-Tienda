using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Tienda.Models;
using System.Security.Cryptography;
using System.Text;

namespace Sistema_de_Tienda.Controllers
{
   [Authorize(Roles = "ADMINISTRADOR")]
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
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Correo,Contrasena,DireccionPrincipal,Dui,FechaNacimiento,Telefono,FechaRegistro,Role")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Contrasena = CalcularHashMD5(cliente.Contrasena);
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(cliente);
        }
        [AllowAnonymous]
        public async Task<IActionResult> CerrarSession()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Cliente cliente)
        {
            cliente.Contrasena = CalcularHashMD5(cliente.Contrasena);
            var userAuth = await _context.
                Clientes
                .FirstOrDefaultAsync(s => s.Correo == cliente.Correo && s.Contrasena == cliente.Contrasena);
            if (userAuth != null && userAuth.Id > 0 && userAuth.Correo == cliente.Correo)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, userAuth.Correo),
                    new Claim("Id", userAuth.Id.ToString()),
                    new Claim("Nombre", userAuth.Nombre),
                    new Claim(ClaimTypes.Role, userAuth.Role)
                    };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "El email o contraseña están incorrectos.");
                return View();
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Correo,DireccionPrincipal,Telefono,Id,Role")] Cliente cliente)
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
                usuarioUpdate.Role = cliente.Role;
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
        private string CalcularHashMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" convierte el byte en una cadena hexadecimal de dos caracteres.
                }
                return sb.ToString();
            }
        }
    }
}
