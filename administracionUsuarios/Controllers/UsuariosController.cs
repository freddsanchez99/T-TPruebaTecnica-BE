using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using administracionUsuarios.Models;

namespace administracionUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public ActionResult<IEnumerable<UserDetailsDto>> GetUserDetails()
        {
            var userDetails = _context.Users
                .Include(u => u.IdDepartamentoNavigation)
                .Include(u => u.IdCargoNavigation)
                .Select(u => new UserDetailsDto
                {
                    Id = u.Id,
                    Usuario = u.Usuario,
                    PrimerNombre = u.PrimerNombre,
                    SegundoNombre = u.SegundoNombre,
                    PrimerApellido = u.PrimerApellido,
                    SegundoApellido = u.SegundoApellido,
                    Departamento = new DepartamentoDto
                    {
                        Id = u.IdDepartamentoNavigation.Id,
                        Codigo = u.IdDepartamentoNavigation.Codigo,
                        Nombre = u.IdDepartamentoNavigation.Nombre,
                        Activo = u.IdDepartamentoNavigation.Activo,
                        IdUsuarioCreacion = u.IdDepartamentoNavigation.IdUsuarioCreacion
                    },
                    Cargo = new CargoDto
                    {
                        Id = u.IdCargoNavigation.Id,
                        Codigo = u.IdCargoNavigation.Codigo,
                        Nombre = u.IdCargoNavigation.Nombre,
                        Activo = u.IdCargoNavigation.Activo,
                        IdUsuarioCreacion = u.IdCargoNavigation.IdUsuarioCreacion
                    }
                })
                .ToList();

            return Ok(userDetails);
        }
    

    // GET: api/Usuarios/5
    [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsuario(int id)
        {
            var usuario = await _context.Users.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<User>> PostUsuario(User usuario)
        {
            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, User usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Users.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Users.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public DepartamentoDto Departamento { get; set; } = new DepartamentoDto();
        public CargoDto Cargo { get; set; } = new CargoDto();
    }

    public class DepartamentoDto
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public bool? Activo { get; set; }
        public int? IdUsuarioCreacion { get; set; }
    }

    public class CargoDto
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public bool? Activo { get; set; }
        public int? IdUsuarioCreacion { get; set; }
    }
}