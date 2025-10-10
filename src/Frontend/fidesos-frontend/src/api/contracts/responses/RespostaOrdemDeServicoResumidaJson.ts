// Define o formato resumido de uma OS para a lista
export interface RespostaOrdemDeServicoResumidaJson {
  id: string; // O Guid da OS (OsIdentificacao)
  descricao: string;
  dataAgendamento: string; // Virá como string no formato ISO 8601 UTC
  status: number; // O número do Enum (0 para Pendente, 1 para Agendada, etc.)
}
