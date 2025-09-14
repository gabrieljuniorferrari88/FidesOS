using FidesOS.Aplicacao.CasoDeUsos.FazerLogin;
using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace FidesOS.Api.Controllers;
public class LoginController : FidesOSControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IFazerLoginCasoDeUso useCase,
        [FromBody] RequisicaoLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
