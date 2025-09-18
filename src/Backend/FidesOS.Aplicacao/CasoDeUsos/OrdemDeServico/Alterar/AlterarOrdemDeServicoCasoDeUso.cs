using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using System;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Alterar;

public class AlterarOrdemDeServicoCasoDeUso : IAlterarOrdemDeServicoCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioAlteracaoOrdemDeServico _alterarOrdemDeServico;
  private readonly IUnitOfWork _unitOfWork;

  public AlterarOrdemDeServicoCasoDeUso(
    IUsuarioLogado usuarioLogado, 
    IRepositorioAlteracaoOrdemDeServico alterarOrdemDeServico, 
    IUnitOfWork unitOfWork)
  {
    _usuarioLogado = usuarioLogado;
    _alterarOrdemDeServico = alterarOrdemDeServico;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(Guid osId, RequisicaoAlterarOrdemDeServicoJson request)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    var os = await _alterarOrdemDeServico.BuscarPorId(osId);

    Validate(os, usuarioLogado.UserIdentificacao, request);

    os!.AtualizarOrdemServico(request.Descricao, request.DataAgendamento);

    await _unitOfWork.Commit();
  }

  private void Validate(Dominio.Entidades.OrdemDeServico? os, Guid userIdentificacao, RequisicaoAlterarOrdemDeServicoJson request)
  {
    if (os is null)
      throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.NAO_ENCONTRADO });

    if (os.GestorIdentificacao != userIdentificacao)
      throw new UnauthorizedException(ResourceMensagensExcecao.USUARIO_SEM_PERMISSAO_ACESSAR_RECURSO);

    switch (os.Status)
    {
      case StatusOS.Concluida:
      case StatusOS.Cancelada:
        throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_CANCELADA });

      case StatusOS.EmAndamento:
        if (os.Descricao != request.Descricao || os.DataAgendamento != request.DataAgendamento)
          throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_EM_ANDAMENTO });
        break;

      case StatusOS.Agendada:
        if (os.DataAgendamento != request.DataAgendamento)
          throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.OS_NAO_PODE_SER_ALTERADA_AGENDADA });
        break;
    }

    var validacao = new AlterarOrdemDeServicoValidacao().Validate(request);

    if (validacao.IsValid.IsFalse())
      throw new ErrorOnValidationException(validacao.Errors.Select(e => e.ErrorMessage).ToList());
  }
}

