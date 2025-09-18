using Microsoft.AspNetCore.Mvc;
using FidesOS.Aplicacao.CasoDeUsos.Token.RefreshToken;
using FidesOS.Comunicacao.Requisicoes.Token;
using FidesOS.Comunicacao.Respostas.Token;

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
