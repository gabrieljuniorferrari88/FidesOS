using FidesOS.Comunicacao.Requisicoes.Usuario;
using FidesOS.Dominio.Extencoes;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Alterar;

public class AlterarUsuarioValidator : AbstractValidator<RequisicaoAlterarUsuarioJson>
{
  public AlterarUsuarioValidator()
  {
    RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceMensagensExcecao.NOME_EM_BRANCO);
    RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMensagensExcecao.EMAIL_EM_BRANCO);
    When(request => request.Email.NotEmpty(), () =>
    {
      RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMensagensExcecao.EMAIL_INVALIDO);
    });
  }
}