using Microsoft.AspNetCore.Mvc;
using Examen_Parcial.Models;
using Examen_Parcial.Services;

namespace Examen_Parcial.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamosController : ControllerBase
{
    private readonly PrestamoService _prestamoService;

    public PrestamosController(PrestamoService prestamoService)
    {
        _prestamoService = prestamoService;
    }

    [HttpPost("crear")]
    public async Task<IActionResult> CrearPrestamo([FromBody] Prestamo prestamo)
    {
        try
        {
            var id = await _prestamoService.CrearPrestamoAsync(prestamo);
            return Ok(new { mensaje = "Préstamo registrado correctamente", id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}