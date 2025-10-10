using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.DetalheProducao.Adicionar;
using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.AlocarTrabalhador;

public class AlocarTrabalhadorValidacao : AbstractValidator<RequisicaoAlocarTrabalhadorJson>
{
  public AlocarTrabalhadorValidacao()
  {
    RuleFor(x => x.ValorCombinado)
      .NotEmpty().GreaterThanOrEqualTo(0).WithMessage(ResourceMensagensExcecao.VALOR_MENOR_OU_IGUAL_ZERO);

    RuleForEach(x => x.Detalhes)
            .SetValidator(new AdicionarDetalheProducaoValidacao());
  }
}