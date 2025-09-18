using FidesOS.Aplicacao.ValidadoresCompatilhados;
using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;

public class CriarOrdemDeServicoValidacao : AbstractValidator<RequisicaoOrdemDeServicoJson>
{
  public CriarOrdemDeServicoValidacao()
  {
    RuleFor(x => x.Descricao).NotEmpty().WithMessage(ResourceMensagensExcecao.DESCRICAO_INVALIDA);
    RuleFor(x => x.DataAgendamento).SetValidator(new ValidadorDeDataAgendamento<RequisicaoOrdemDeServicoJson>());
  }
}
