using FidesOS.Aplicacao.CasoDeUsos.Senha.Recuperar;
using FidesOS.Aplicacao.CasoDeUsos.Senha.Resetar;
using FidesOS.Comunicacao.Requisicoes.Senha;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Comunicacao.Respostas.Senha;
using Microsoft.AspNetCore.Mvc;

namespace FidesOS.Api.Controllers;

public class SenhaController : FidesOSControllerBase
{
  [HttpPost("recuperar")]
  [ProducesResponseType(typeof(RespostaRecuperarSenhaJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> RecuperarSenha(
    [FromServices] IRecuperarSenhaCasoDeUso useCase, 
    [FromBody] RequisicaoRecuperarSenhaJson request)
  {
    var response = await useCase.Execute(request);

    return Created(string.Empty, response);
  }

  [HttpPost("resetar")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> ResetarSenha(
    [FromServices] IResetarSenhaCasoDeUso useCase,
    [FromBody] RequisicaoResetarSenhaJson request)
  {
    await useCase.Execute(request);

    return NoContent();
  }
}
