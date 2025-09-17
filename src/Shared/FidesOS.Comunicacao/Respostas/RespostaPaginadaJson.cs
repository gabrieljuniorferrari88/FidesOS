namespace FidesOS.Comunicacao.Respostas;

public class RespostaPaginadaJson<T> where T : class
{
  public IList<T> Itens { get; set; }
  public int TotalDePaginas { get; set; }
  public int PaginaAtual { get; set; }
  public int TotalDeItens { get; set; }
}
