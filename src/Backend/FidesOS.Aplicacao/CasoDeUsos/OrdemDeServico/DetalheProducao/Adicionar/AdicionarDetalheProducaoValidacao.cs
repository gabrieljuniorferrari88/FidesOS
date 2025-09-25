using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.DetalhesProducao;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.DetalheProducao.Adicionar;

public class AdicionarDetalheProducaoValidacao : AbstractValidator<RequisicaoDetalheProducaoJson>
{
  public AdicionarDetalheProducaoValidacao()
  {
    RuleFor(x => x.Descricao).NotEmpty().WithMessage(ResourceMensagensExcecao.DESCRICAO_INVALIDA);
    RuleFor(x => x.Valor).NotEmpty().GreaterThanOrEqualTo(0).WithMessage(ResourceMensagensExcecao.VALOR_MENOR_OU_IGUAL_ZERO);
  }
}