import api from '@/lib/axios';
import { RespostaOrdemDeServicoResumidaJson, RespostaPaginadaJson } from '@/types/api-types';

interface ListarOSParams {
  page: number;
  perPage: number;
  status?: string;
  descricao?: string;
}

export const listarOrdensDeServico = async (params: ListarOSParams, token?: string) => {

  const config = token ? {
    params: params,
    headers: {
      Authorization: `Bearer ${token}`
    }
  } : {
    params: params
  };

  const response = await api.get<RespostaPaginadaJson<RespostaOrdemDeServicoResumidaJson>>('/ordemdeservico',
    config
  );

  return response.data;
};

export const cancelarOrdemDeServico = async (osId: string) => {
  await api.delete(`/ordemdeservico/${osId}`);
};