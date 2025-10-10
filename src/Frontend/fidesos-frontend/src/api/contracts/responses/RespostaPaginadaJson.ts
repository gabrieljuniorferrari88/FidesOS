// Define a estrutura de uma resposta paginada
export interface RespostaPaginadaJson<T> {
  itens: T[];
  totalDePaginas: number;
  paginaAtual: number;
  totalDeItens: number;
}
