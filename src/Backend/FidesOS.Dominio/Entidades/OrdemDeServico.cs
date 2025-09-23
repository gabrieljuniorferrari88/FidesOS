using FidesOS.Dominio.Enums;

namespace FidesOS.Dominio.Entidades;
public class OrdemDeServico : EntidadeBase
{
  public Guid OsIdentificacao { get; protected set; } = Guid.CreateVersion7();
  public Guid GestorIdentificacao { get; protected set; } // FK para o Gestor
  public Guid EmpresaClienteId { get; protected set; } // FK para o Usuario "Empresa"

  public DateTime DataAgendamento { get; protected set; }
  public StatusOS Status { get; protected set; }
  public string Descricao { get; protected set; } = string.Empty;

  // TODO: Adicionar a lista de Alocacoes (propriedade de navegação)
  public void AtualizarOrdemServico(string novaDescricao, DateTime novaDataAgendamento)
  {
    Descricao = novaDescricao;
    DataAgendamento = novaDataAgendamento;
  }

  public void AdicionarOGestorId(Guid gestorId)
  {
    GestorIdentificacao = gestorId;
  }

  public void CancelarOrdemServico()
  {
    Status = StatusOS.Cancelada;
  }
}