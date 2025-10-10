import api from "..";

// Importamos os "contratos" de resposta que acabamos de criar
import {
	RespostaOrdemDeServicoDetalhadaJson,
	RespostaOrdemDeServicoResumidaJson,
	RespostaPaginadaJson
} from "../contracts/responses/index";

/**
 * Função que busca as Ordens de Serviço paginadas na API.
 * @param pagina O número da página a ser buscada.
 * @param itensPorPagina A quantidade de itens por página.
 * @returns Uma promessa com a resposta paginada da API.
 */
export const listarOrdensDeServico = async (pagina: number, itensPorPagina: number) => {
  // Usamos nossa instância 'api' e chamamos o método GET
  const response = await api.get<RespostaPaginadaJson<RespostaOrdemDeServicoResumidaJson>>('/ordemdeservico', {
    // O objeto 'params' do axios converte automaticamente para a query string na URL
    // ex: /ordemdeservico?pagina=1&itensPorPagina=10
    params: {
      pagina,
      itensPorPagina,
    }
  });

  // Retornamos apenas a parte 'data' da resposta, que contém o nosso JSON
  return response.data;
};

export const buscarOsDetalhada = async (osId: string) => {
  // Faz a chamada para o endpoint específico, passando o ID na URL
  const response = await api.get<RespostaOrdemDeServicoDetalhadaJson>(`/ordemdeservico/${osId}`);
  return response.data;
};

export const cancelarOrdemDeServico = async (osId: string) => {
  await api.delete(`/ordemdeservico/${osId}`);
};
