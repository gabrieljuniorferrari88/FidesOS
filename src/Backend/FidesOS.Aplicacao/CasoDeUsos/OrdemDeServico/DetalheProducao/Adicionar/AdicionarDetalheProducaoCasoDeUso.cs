using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.DetalhesProducao;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.DetalhesProducao;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.DetalheProducao.Adicionar;

public class AdicionarDetalheProducaoCasoDeUso : IAdicionarDetalheProducaoCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioAlteracaoAlocacao _repositorioAlteracaoAlocacao;
  private readonly IRepositorioEscritaDetalheProducao _repositorioEscritaDetalheProducao;
  private readonly IUnitOfWork _unitOfWork;

  public AdicionarDetalheProducaoCasoDeUso(
    IUsuarioLogado usuarioLogado,
    IRepositorioAlteracaoAlocacao repositorioAlteracaoAlocacao,
    IRepositorioEscritaDetalheProducao repositorioEscritaDetalheProducao,
    IUnitOfWork unitOfWork
    )
  {
    _usuarioLogado = usuarioLogado;
    _repositorioAlteracaoAlocacao = repositorioAlteracaoAlocacao;
    _repositorioEscritaDetalheProducao = repositorioEscritaDetalheProducao;
    _unitOfWork = unitOfWork;
  }

  public async Task<RespostaDetalheProducaoJson> Execute(RequisicaoDetalheProducaoJson request, Guid osId, Guid alocacaoId)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    var alocacaoTrabalhador = await Validate(request, usuarioLogado.UserIdentificacao, osId, alocacaoId);

    var detalheProducao = new Dominio.Entidades.DetalheProducao(alocacaoId, request.Descricao, request.Valor);

    alocacaoTrabalhador!.AdicionarCusto(request.Valor);

    await _repositorioEscritaDetalheProducao.AddAsync(detalheProducao);

    await _unitOfWork.Commit();

    return detalheProducao.Adapt<RespostaDetalheProducaoJson>();
  }

  private async Task<AlocacaoTrabalhador> Validate(
    RequisicaoDetalheProducaoJson request,
    Guid userLogado,
    Guid osId,
    Guid alocacaoId
    )
  {
    var alocacaoTrabalhador = await _repositorioAlteracaoAlocacao.BuscarPorId(alocacaoId)
     ?? throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.NAO_ENCONTRADO });

    if (alocacaoTrabalhador.OrdemDeServico.OsIdentificacao != osId)
      throw new UnauthorizedException(ResourceMensagensExcecao.NAO_ENCONTRADO);

    if (alocacaoTrabalhador.OrdemDeServico.GestorIdentificacao != userLogado)
      throw new UnauthorizedException(ResourceMensagensExcecao.USUARIO_SEM_PERMISSAO_ACESSAR_RECURSO);

    switch (alocacaoTrabalhador.OrdemDeServico.Status)
    {
      case StatusOS.Concluida:
        throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_CONCLUIDA });

      case StatusOS.Cancelada:
        throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_CANCELADA });
    }

    var resultado = new AdicionarDetalheProducaoValidacao().Validate(request);

    if (resultado.IsValid.IsFalse())
      throw new ErrorOnValidationException(resultado.Errors.Select(e => e.ErrorMessage).ToList());

    return alocacaoTrabalhador;
  }
}