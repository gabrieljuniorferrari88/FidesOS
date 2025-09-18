using FidesOS.Aplicacao.ValidadoresCompatilhados;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Alterar;

public class AlterarOrdemDeServicoValidacao : AbstractValidator<RequisicaoAlterarOrdemDeServicoJson>
{
  public AlterarOrdemDeServicoValidacao()
  {
    RuleFor(x => x.Descricao).NotEmpty().WithMessage(ResourceMensagensExcecao.DESCRICAO_INVALIDA);
    RuleFor(x => x.DataAgendamento).SetValidator(new ValidadorDeDataAgendamento<RequisicaoAlterarOrdemDeServicoJson>());
  }
}
