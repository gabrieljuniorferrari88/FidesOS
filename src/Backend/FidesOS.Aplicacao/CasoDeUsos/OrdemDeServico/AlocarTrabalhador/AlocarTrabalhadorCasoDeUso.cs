using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Comunicacao.Respostas.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using FluentValidation.Results;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.AlocarTrabalhador;

public class AlocarTrabalhadorCasoDeUso : IAlocarTrabalhadorCasoDeUso
{
  private readonly IRepositorioAlteracaoOrdemDeServico _repositorioAlteracaoOrdemDeServico;
  private readonly IRepositorioEscritaAlocacao _repositorioEscritaAlocacao;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioLeituraUsuario _repositorioLeituraUsuario;

  public AlocarTrabalhadorCasoDeUso(
    IUsuarioLogado usuarioLogado,
    IUnitOfWork unitOfWork,
    IRepositorioEscritaAlocacao repositorioEscritaAlocacao,
    IRepositorioAlteracaoOrdemDeServico repositorioAlteracaoOrdemDeServico,
    IRepositorioLeituraUsuario repositorioLeituraUsuario)
  {
    _usuarioLogado = usuarioLogado;
    _unitOfWork = unitOfWork;
    _repositorioEscritaAlocacao = repositorioEscritaAlocacao;
    _repositorioAlteracaoOrdemDeServico = repositorioAlteracaoOrdemDeServico;
    _repositorioLeituraUsuario = repositorioLeituraUsuario;
  }

  public async Task<RespostaAlocacaoJson> Execute(RequisicaoAlocarTrabalhadorJson request, Guid osId)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    var (osValidada, trabalhadorValidado) = await Validate(request, usuarioLogado.UserIdentificacao, osId);

    var novaAlocacao = new AlocacaoTrabalhador(osId, request.TrabalhadorIdentificacao, request.ValorCombinado);

    await _repositorioEscritaAlocacao.AddAsync(novaAlocacao);

    await _unitOfWork.Commit();

    return novaAlocacao.Adapt<RespostaAlocacaoJson>();
  }

  private async Task<(Dominio.Entidades.OrdemDeServico os, Dominio.Entidades.Usuario trabalhador)> Validate(
    RequisicaoAlocarTrabalhadorJson request,
    Guid userIdentificacao,
    Guid osId)
  {
    var os = await _repositorioAlteracaoOrdemDeServico.BuscarPorId(osId)
      ?? throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.NAO_ENCONTRADO });

    if (os.GestorIdentificacao != userIdentificacao)
      throw new UnauthorizedException(ResourceMensagensExcecao.USUARIO_SEM_PERMISSAO_ACESSAR_RECURSO);

    switch (os.Status)
    {
      case StatusOS.Concluida:
        throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_CONCLUIDA });

      case StatusOS.Cancelada:
        throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_CANCELADA });
    }

    var resultado = new AlocarTrabalhadorValidacao().Validate(request);

    var trabalhador = await _repositorioLeituraUsuario.BuscarPorUserIdentificacao(request.TrabalhadorIdentificacao)
      ?? throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.NAO_ENCONTRADO_TRABALHADOR });

    if (trabalhador.GestorIdentificacao != userIdentificacao)
      resultado.Errors.Add(new ValidationFailure(string.Empty, ResourceMensagensExcecao.TRABALHADOR_NAO_PERTENCE_AO_GESTOR));

    if (trabalhador.Perfil != PerfilUsuario.Trabalhador)
      resultado.Errors.Add(new ValidationFailure(string.Empty, ResourceMensagensExcecao.NAO_ENCONTRADO_TRABALHADOR));

    if (trabalhador.Status == StatusUsuario.Pendente || trabalhador.Status == StatusUsuario.Inativo)
      resultado.Errors.Add(new ValidationFailure(string.Empty, ResourceMensagensExcecao.TRABALHADOR_INATIVO_PENDENTE));

    if (resultado.IsValid.IsFalse())
      throw new ErrorOnValidationException(resultado.Errors.Select(e => e.ErrorMessage).ToList());

    return (os, trabalhador);
  }
}