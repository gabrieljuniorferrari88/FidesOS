using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Cancelar;

public class CancelarOrdemDeServicoCasoDeUso : ICancelarOrdemDeServicoCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRepositorioAlteracaoOrdemDeServico _ordemDeServico;

  public CancelarOrdemDeServicoCasoDeUso(
    IUsuarioLogado usuarioLogado,
    IUnitOfWork unitOfWork,
    IRepositorioAlteracaoOrdemDeServico ordemDeServico)
  {
    _usuarioLogado = usuarioLogado;
    _unitOfWork = unitOfWork;
    _ordemDeServico = ordemDeServico;
  }

  public async Task Execute(Guid osId)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    var os = await _ordemDeServico.BuscarPorId(osId);

    Validate(os, usuarioLogado.UserIdentificacao);

    os!.CancelarOrdemServico();

    await _unitOfWork.Commit();
  }

  private void Validate(Dominio.Entidades.OrdemDeServico? os, Guid userIdentificacao)
  {
    if (os is null)
      throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_EXISTE });

    if (os.GestorIdentificacao != userIdentificacao)
      throw new UnauthorizedException(ResourceMensagensExcecao.USUARIO_SEM_PERMISSAO_ACESSAR_RECURSO);

    switch (os.Status)
    {
      case StatusOS.Cancelada:
        throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_CANCELADA });

      case StatusOS.Concluida:
        throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_CONCLUIDA });
    }
  }
}