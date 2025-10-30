export interface RequisicaoOrdemDeServicoJson {
  empresaClienteId?: string;
  descricao?: string;
  dataAgendamento?: string;
}

export interface ListarOSParams {
  page: number;
  perPage: number;
  status?: string;
  descricao?: string;
}

export interface RequisicaoAlterarOrdemDeServicoJson {
  descricao?: string;
  dataAgendamento?: string;
  empresaClienteId?: string;
}