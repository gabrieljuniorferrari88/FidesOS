using FidesOS.Api.Atributos;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Alterar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Buscar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Cancelar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.DetalheProducao.Adicionar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Listar;
using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.DetalhesProducao;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using FidesOS.Comunicacao.Respostas.OrdemDeServico.AlocarTrabalhador;
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

  [HttpDelete("{osId}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> CancelarOrdemServico(
    Guid osId,
    [FromServices] ICancelarOrdemDeServicoCasoDeUso useCase
    )
  {
    await useCase.Execute(osId);

    return NoContent();
  }

  [HttpGet("{osId}")]
  [ProducesResponseType(typeof(RespostaOrdemDeServicoDetalhadaJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> BuscarOrdemServicoPorId(
    Guid osId,
    [FromServices] IBuscarOrdemDeServicoCasoDeUso useCase
    )
  {
    var response = await useCase.Execute(osId);

    return Ok(response);
  }

  //Alocando trabalhador
  [HttpPost("{osId}/alocar")]
  [ProducesResponseType(typeof(RespostaAlocacaoJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> AdicionarTrabalhadorNaOrdemServico(
    [FromServices] IAlocarTrabalhadorCasoDeUso useCase,
    [FromBody] RequisicaoAlocarTrabalhadorJson request,
    Guid osId)
  {
    var response = await useCase.Execute(request, osId);

    return Created(string.Empty, response);
  }

  [HttpPost("{osId}/alocacao/{alocacaoId}/adicionar-detalhe")]
  [ProducesResponseType(typeof(RespostaDetalheProducaoJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(RespostaErrorJson), StatusCodes.Status400BadRequest)]
  [UsuarioAutenticado]
  public async Task<IActionResult> AdicionarTrabalhadorNaOrdemServico(
    [FromServices] IAdicionarDetalheProducaoCasoDeUso useCase,
    [FromBody] RequisicaoDetalheProducaoJson request,
    Guid osId,
    Guid alocacaoId)
  {
    var response = await useCase.Execute(request, osId, alocacaoId);

    return Created(string.Empty, response);
  }
}