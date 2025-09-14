using Microsoft.AspNetCore.Mvc;
using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Aplicacao.CasoDeUsos.Token.RefreshToken;

namespace FidesOS.Api.Controllers;
public class AuthenticationController : FidesOSControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(RespostaTokensJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh(
        [FromServices] IUseRefreshTokenUseCase useCase,
        [FromBody] RequisicaoNovoTokenJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
