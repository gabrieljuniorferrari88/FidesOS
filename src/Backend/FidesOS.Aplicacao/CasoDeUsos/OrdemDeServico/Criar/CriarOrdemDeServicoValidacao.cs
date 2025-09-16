using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;

public class CriarOrdemDeServicoValidacao : AbstractValidator<RequisicaoOrdemDeServicoJson>
{
  public CriarOrdemDeServicoValidacao()
  {
    RuleFor(x => x.Descricao).NotEmpty().WithMessage(ResourceMensagensExcecao.DESCRICAO_INVALIDA);
    RuleFor(x => x.DataAgendamento)
            .NotEmpty()
            .WithMessage(ResourceMensagensExcecao.DATA_INVALIDA)
            .Must(data => data > DateTime.Now)
            .WithMessage(ResourceMensagensExcecao.DATA_NO_PASSADO)
            .Must(data => data >= DateTime.Now.AddHours(3))
            .WithMessage(ResourceMensagensExcecao.DATA_INFERIOR_3_HORAS);
  }
}
