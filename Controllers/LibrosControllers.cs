using Microsoft.AspNetCore.Mvc;
using Examen_Parcial.Models;
using Examen_Parcial.Services;

namespace Examen_Parcial.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibrosController : ControllerBase
{
    private readonly LibrosService _librosService;

    public LibrosController(LibrosService librosService)
    {
        _librosService = librosService;
    }

    [HttpPost("crear")]
    public async Task<IActionResult> CrearLibro([FromBody] Libros libro)
    {
        try
        {
            var id = await _librosService.CrearLibroAsync(libro);
            return Ok(new { mensaje = "Libro agregado correctamente", id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}