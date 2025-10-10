using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Aplicacao.ValidadoresCompatilhados;
using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Excecao;
using FluentValidation;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.CriarCompleta;

public class CriarOrdemDeServicoCompletaValidacao : AbstractValidator<RequisicaoOrdemDeServicoCompletaJson>
{
  public CriarOrdemDeServicoCompletaValidacao()
  {
    RuleFor(x => x.EmpresaClienteId).NotEmpty().WithMessage(ResourceMensagensExcecao.EMPRESA_NAO_ENCONTRADO);// "ID da empresa cliente é obrigatório.");
    RuleFor(x => x.DataAgendamento).SetValidator(new ValidadorDeDataAgendamento<RequisicaoOrdemDeServicoCompletaJson>());

    RuleForEach(x => x.Alocacoes)
        .SetValidator(new AlocarTrabalhadorValidacao());
  }
}