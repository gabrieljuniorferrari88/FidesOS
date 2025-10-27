export interface RespostaPaginadaJson<T> {
  itens: T[];
  totalDePaginas: number;
  paginaAtual: number;
  totalDeItens: number;
}

export interface RespostaOrdemDeServicoResumidaJson {
  id: string; 
  descricao: string;
  dataAgendamento: string; 
  status: number;
}
