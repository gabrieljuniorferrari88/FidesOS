using FidesOS.Api.Atributos;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Alterar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Listar;
using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using Microsoft.AspNetCore.Mvc;

namespace FidesOS.Api.Controllers;

public class OrdemDeServicoController : FidesOSControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(RespostaOrdemDeServicoJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> CriarOrdemServico(
    [FromServices] ICriarOrdemDeServicoCasoDeUso useCase,
    [FromBody] RequisicaoOrdemDeServicoJson request)
  {
    var response = await useCase.Execute(request);

    return Created(string.Empty, response);
  }

  [HttpGet]
  [ProducesResponseType(typeof(RespostaPaginadaJson<RespostaOrdemDeServicoResumidaJson>), StatusCodes.Status200OK)]
  [UsuarioAutenticado]
  public async Task<IActionResult> ListarOrdemServico(
    [FromServices] IListarOrdensDeServicoCasoDeUso useCase,
    [FromQuery] int pagina,
    [FromQuery] int itensPorPagina)
  {
    var response = await useCase.Execute(pagina, itensPorPagina);

    return Ok(response);
  }

  [HttpPut("{osId}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> AlterarOrdemServico(
    Guid osId,
    [FromServices] IAlterarOrdemDeServicoCasoDeUso useCase,
    [FromBody] RequisicaoAlterarOrdemDeServicoJson request)
  {
    await useCase.Execute(osId, request);

    return NoContent();
  }
}
