using Microsoft.AspNetCore.Mvc;
using Examen_Parcial.Models;
using Examen_Parcial.Dtos;
using Examen_Parcial.Services;

namespace Examen_Parcial.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // ✅ REGISTRO
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Users usuario)
    {
        try
        {
            var id = await _authService.RegistrarAsync(usuario);
            return Ok(new { mensaje = "Usuario registrado correctamente", id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // ✅ LOGIN
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = await _authService.LoginAsync(dto.Correo, dto.Contrasena);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}