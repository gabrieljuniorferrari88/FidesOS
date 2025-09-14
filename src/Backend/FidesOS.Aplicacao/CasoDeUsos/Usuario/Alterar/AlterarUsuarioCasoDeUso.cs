using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using FluentValidation.Results;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Alterar;

public class AlterarUsuarioCasoDeUso : IAlterarUsuarioCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRepositorioAlteracaoUsuario _repository;
  private readonly IRepositorioLeituraUsuario _repositorioLeitura;

  public AlterarUsuarioCasoDeUso(
    IUsuarioLogado usuarioLogado,
      IUnitOfWork unitOfWork,
      IRepositorioAlteracaoUsuario repository,
      IRepositorioLeituraUsuario repositorioLeitura)
  {
    _usuarioLogado = usuarioLogado;
    _unitOfWork = unitOfWork;
    _repository = repository;
    _repositorioLeitura = repositorioLeitura;
  }

  public async Task Execute(RequisicaoAlterarUsuarioJson request)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    await Validate(request, usuarioLogado.Email);

    var usuario = await _repository.BuscarPorUserIdentificacao(usuarioLogado.UserIdentificacao);

    usuario.AtualizarPerfil(request.Nome, request.Email);

    //_repository.Update(usuario);

    await _unitOfWork.Commit();
  }

  private async Task Validate(RequisicaoAlterarUsuarioJson request, string emailAtual)
  {
    var validacao = new AlterarUsuarioValidator().Validate(request);

    if (emailAtual.Equals(request.Email).IsFalse())
    {
      var userExist = await _repositorioLeitura.ExisteUsuarioComEmail(request.Email);
      if (userExist)
        validacao.Errors.Add(new ValidationFailure(string.Empty, ResourceMensagensExcecao.EMAIL_EXISTENTE));
    }

    if (validacao.IsValid.IsFalse())
    {
      var errorMessages = validacao.Errors.Select(error => error.ErrorMessage).ToList();

      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
