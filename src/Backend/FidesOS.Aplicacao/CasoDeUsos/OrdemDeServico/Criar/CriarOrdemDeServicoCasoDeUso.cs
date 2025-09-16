using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using FluentValidation.Results;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;

public class CriarOrdemDeServicoCasoDeUso : ICriarOrdemDeServicoCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioEscritaOrdemDeServico _repositorioEscrita;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRepositorioLeituraUsuario _repositorioLeituraUsuario;

  public CriarOrdemDeServicoCasoDeUso(
    IUsuarioLogado usuarioLogado,
    IRepositorioEscritaOrdemDeServico repositorioEscrita,
    IUnitOfWork unitOfWork,
    IRepositorioLeituraUsuario repositorioLeituraUsuario)
  {
    _usuarioLogado = usuarioLogado;
    _repositorioEscrita = repositorioEscrita;
    _unitOfWork = unitOfWork;
    _repositorioLeituraUsuario = repositorioLeituraUsuario;
  }

  public async Task<RespostaOrdemDeServicoJson> Execute(RequisicaoOrdemDeServicoJson request)
  {

    await Validate(request);

    var usuarioLogado = await _usuarioLogado.Get();

    var novaOrdem = request.Adapt<Dominio.Entidades.OrdemDeServico>();

    novaOrdem.AdicionarOGestorId(usuarioLogado.GestorIdentificacao);

    await _repositorioEscrita.AddAsync(novaOrdem);

    await _unitOfWork.Commit();

    return new()
    {
      DataAgendamento = novaOrdem.DataAgendamento,
      Descricao = novaOrdem.Descricao,
      Status = novaOrdem.Status,
      Id = novaOrdem.OsIdentificacao,
    };
  }

  private async Task Validate(RequisicaoOrdemDeServicoJson request)
  {
    var result = new CriarOrdemDeServicoValidacao().Validate(request);

    var empresaExiste = await _repositorioLeituraUsuario.ExisteEmpresaComUserIdentificacao(request.EmpresaClienteId);
    if (empresaExiste.IsFalse())
      result.Errors.Add(new ValidationFailure(string.Empty, ResourceMensagensExcecao.EMPRESA_NAO_ENCONTRADO));

    if (result.IsValid.IsFalse())
      throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
  }
}
