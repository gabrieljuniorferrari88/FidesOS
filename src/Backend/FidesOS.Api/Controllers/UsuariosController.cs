using Microsoft.AspNetCore.Mvc;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.Registrar;
using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegistrarUsuarioCasoDeUso useCase, [FromBody] RequisicaoRegistrarUsuarioJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
