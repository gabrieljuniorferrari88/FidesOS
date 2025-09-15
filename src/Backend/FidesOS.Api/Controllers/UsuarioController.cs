using FidesOS.Api.Atributos;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.Alterar;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.AlterarSenha;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.Perfil;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.Registrar;
using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace FidesOS.Api.Controllers;

public class UsuarioController : FidesOSControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Registrar([FromServices] IRegistrarUsuarioCasoDeUso useCase, [FromBody] RequisicaoRegistrarUsuarioJson request)
  {
    var response = await useCase.Execute(request);

    return Created(string.Empty, response);
  }

  [HttpGet]
  [ProducesResponseType(typeof(RespostaUsuarioPerfilJson), StatusCodes.Status200OK)]
  [UsuarioAutenticado]
  public async Task<IActionResult> BuscarPerfil([FromServices] IBuscarPerfilUsuarioCasoDeUso useCase)
  {
    var response = await useCase.Execute();

    return Ok(response);
  }

  [HttpPut]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> AtualizarPerfil([FromServices] IAlterarUsuarioCasoDeUso useCase, [FromBody] RequisicaoAlterarUsuarioJson request)
  {
    await useCase.Execute(request);

    return NoContent();
  }

  [HttpPut("alterar-senha")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> AtualizarSenha([FromServices] IAlterarSenhaUsuarioCasoDeUso useCase, [FromBody] RequisicaoAlterarSenhaUsuarioJson request)
  {
    await useCase.Execute(request);

    return NoContent();
  }
}
