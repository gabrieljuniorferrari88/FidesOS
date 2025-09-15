using FidesOS.Aplicacao.ValidadoresCompatilhados;
using FidesOS.Comunicacao.Requisicoes;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.AlterarSenha;

public class AlterarSenhaUsuarioValidacao : AbstractValidator<RequisicaoAlterarSenhaUsuarioJson>
{
  public AlterarSenhaUsuarioValidacao()
  {
    RuleFor(x => x.NovaSenha).SetValidator(new ValidadorDeSenha<RequisicaoAlterarSenhaUsuarioJson>());
  }
}
