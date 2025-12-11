using Microsoft.AspNetCore.Mvc;
using Examen_Parcial.Models;
using Examen_Parcial.Services;

namespace Examen_Parcial.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservaService _reservaService;

    public ReservasController(ReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpPost("crear")]
    public async Task<IActionResult> CrearReserva([FromBody] Reserva reserva)
    {
        try
        {
            var id = await _reservaService.CrearReservaAsync(reserva);
            return Ok(new { mensaje = "Reserva creada correctamente", id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}