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

export interface RespostaDetalheProducaoJson {
  id: string;
  descricao: string;
  valor: number;
}

export interface RespostaAlocacaoTrabalhadorJson {
  id: string;
  trabalhadorIdentificacao: string;
  valorTotal: number;
  detalhes: RespostaDetalheProducaoJson[];
}

export interface RespostaOrdemDeServicoDetalhadaJson {
  id: string;
  empresaClienteId?: string;
  descricao?: string;
  dataAgendamento?: string;
  status: number;
  alocacoes: RespostaAlocacaoTrabalhadorJson[];
}

export interface RespostaOrdemDeServicoJson {
  id: string;
  descricao?: string;
  dataAgendamento?: string;
  status: number;
}