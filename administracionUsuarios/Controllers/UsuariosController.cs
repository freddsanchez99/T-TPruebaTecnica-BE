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

        [HttpPost]
        public async Task<ActionResult<User>> PostUsuario(User usuario)
        {
            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpPut]
        public async Task<ActionResult> Put(User usuarios)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Usuario == usuarios.Usuario);

            if (existingUser == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            existingUser.Usuario = usuarios.Usuario;
            existingUser.PrimerNombre = usuarios.PrimerNombre;
            existingUser.SegundoNombre = usuarios.SegundoNombre;
            existingUser.PrimerApellido = usuarios.PrimerApellido;
            existingUser.SegundoApellido = usuarios.SegundoApellido;
            existingUser.IdDepartamento = usuarios.IdDepartamento;
            existingUser.IdCargo = usuarios.IdCargo;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { Error = "Error al actualizar usuario: " + ex.Message });
            }
        }

        [HttpDelete("{usuario}")]
        public async Task<IActionResult> Delete(string usuario)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Usuario == usuario);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("ByDepartamento/{codigoDepartamento}")]
        public ActionResult<IEnumerable<UserDetailsDto>> GetUsuariosByDepartamento(string codigoDepartamento)
        {
            var usuariosByDepartamento = _context.Users
                .Include(u => u.IdDepartamentoNavigation)
                .Include(u => u.IdCargoNavigation)
                .Where(u => u.IdDepartamentoNavigation.Codigo == codigoDepartamento)
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

            return Ok(usuariosByDepartamento);
        }

        [HttpGet("ByCargo/{codigoCargo}")]
        public ActionResult<IEnumerable<UserDetailsDto>> GetUsuariosByCargo(string codigoCargo)
        {
            var usuariosByCargo = _context.Users
                .Include(u => u.IdDepartamentoNavigation)
                .Include(u => u.IdCargoNavigation)
                .Where(u => u.IdCargoNavigation.Codigo == codigoCargo)
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

            return Ok(usuariosByCargo);
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
