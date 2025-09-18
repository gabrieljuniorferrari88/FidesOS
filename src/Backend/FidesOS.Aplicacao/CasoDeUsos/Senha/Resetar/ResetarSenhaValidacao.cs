using FidesOS.Aplicacao.ValidadoresCompatilhados;
using FidesOS.Comunicacao.Requisicoes.Senha;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Resetar;

public class ResetarSenhaValidacao : AbstractValidator<RequisicaoResetarSenhaJson>
{
  public ResetarSenhaValidacao()
  {
    RuleFor(x => x.TokenDeRecuperacao).NotEmpty().WithMessage(ResourceMensagensExcecao.SEM_TOKEN);
    RuleFor(x => x.NovaSenha).SetValidator(new ValidadorDeSenha<RequisicaoResetarSenhaJson>());
  }
}
