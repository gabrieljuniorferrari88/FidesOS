using FidesOS.Aplicacao.ValidadoresCompatilhados;
using FidesOS.Comunicacao.Requisicoes.Usuario;
using FidesOS.Dominio.Extencoes;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Registrar;

public class RegistrarUsuarioValidator : AbstractValidator<RequisicaoRegistrarUsuarioJson>
{
  public RegistrarUsuarioValidator()
  {
    RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceMensagensExcecao.NOME_EM_BRANCO);
    RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMensagensExcecao.EMAIL_EM_BRANCO);
    RuleFor(request => request.Senha).SetValidator(new ValidadorDeSenha<RequisicaoRegistrarUsuarioJson>());
    When(request => request.Email.NotEmpty(), () =>
    {
      RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMensagensExcecao.EMAIL_INVALIDO);
    });
  }
}