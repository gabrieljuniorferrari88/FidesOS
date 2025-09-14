using FidesOS.Dominio.Enums;

namespace FidesOS.Dominio.Entidades;
public class AlocacaoTrabalhador : EntidadeBase
{
  public Guid AlocacaoIdentificacao { get; protected set; } = Guid.CreateVersion7();
  public Guid OsIdentificacao { get; protected set; } // FK para OrdemDeServico
  public Guid TrabalhadorIdentificacao { get; protected set; } // FK para o Usuario "Trabalhador"

  public DateTime? InicioTrabalho { get; protected set; }
  public DateTime? FimTrabalho { get; protected set; }

  // Valores em centavos
  public long ValorCombinado { get; protected set; }
  public long ValorAcrescimo { get; protected set; }
  public long ValorDesconto { get; protected set; }
  public long ValorTotal { get; protected set; }

  public StatusFaturamento StatusFaturamento { get; protected set; }
  public StatusPagamento StatusPagamento { get; protected set; }

  // TODO: Adicionar a lista de DetalhesProducao (propriedade de navegação)
}
