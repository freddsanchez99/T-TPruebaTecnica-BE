using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using administracionUsuarios.Models;

namespace administracionUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CargosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cargos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargos()
        {
            return await _context.Cargos.ToListAsync();
        }

        private bool CargoExists(int id)
        {
            return _context.Cargos.Any(e => e.Id == id);
        }
    }
}

