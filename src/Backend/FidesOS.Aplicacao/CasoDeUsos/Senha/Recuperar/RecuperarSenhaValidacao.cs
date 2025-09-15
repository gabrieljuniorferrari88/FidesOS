using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Dominio.Extencoes;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Recuperar;

public class RecuperarSenhaValidacao : AbstractValidator<RequisicaoRecuperarSenhaJson>
{
  public RecuperarSenhaValidacao()
  {
    RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMensagensExcecao.EMAIL_EM_BRANCO);
    When(request => request.Email.NotEmpty(), () =>
    {
      RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMensagensExcecao.EMAIL_INVALIDO);
    });
  }
}
