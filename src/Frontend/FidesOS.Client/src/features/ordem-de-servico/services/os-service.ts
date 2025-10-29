import api from '@/lib/axios';
import { RespostaOrdemDeServicoDetalhadaJson, RespostaOrdemDeServicoJson, RespostaOrdemDeServicoResumidaJson, RespostaPaginadaJson } from '@/types/api-resposta';

interface ListarOSParams {
  page: number;
  perPage: number;
  status?: string;
  descricao?: string;
}

interface AlterarOSParams {
  descricao?: string;
  dataAgendamento?: string;
}

interface CriarOSParams {
  empresaClienteId: string; // Guid como string
  descricao: string;
  dataAgendamento: string; // ISO string ou Date formatada
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

export const buscarOrdemServicoPorId = async (osId: string, token?: string) => {

  const config = token ? {
    headers: {
      Authorization: `Bearer ${token}`
    }
  } : {
    headers: {
      Authorization: ""
    }
  };

  const response = await api.get<RespostaOrdemDeServicoDetalhadaJson>(`/ordemdeservico/${osId}`,
    config
  );

  return response.data;
};

export const cancelarOrdemDeServico = async (osId: string) => {
  await api.delete(`/ordemdeservico/${osId}`);
};

import { RespostaErrorJson } from '@/types/api-errors';

export const criarOrdemServico = async (dadosOS: CriarOSParams, token?: string) => {
  try {
    const config = token ? {
      headers: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json',
        'Accept-Language': 'pt-BR'
      }
    } : {
      headers: {
        'Content-Type': 'application/json'
      }
    };

    const response = await api.post<RespostaOrdemDeServicoJson>('/ordemdeservico', dadosOS, config);
    return response.data;
  } catch (error: any) {
    // Podemos fazer tratamento específico aqui se necessário
    if (error.response?.data) {
      // Adiciona a tipagem do erro
      error.apiError = error.response.data as RespostaErrorJson;
    }
    throw error;
  }
};

export const alterarOrdemServico = async (osId: string, dadosOS: AlterarOSParams, token?: string) => {
  try {
    const config = token ? {
      params: {
        osId: osId
      },
      headers: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json',
        'Accept-Language': 'pt-BR'
      }
    } : {
      headers: {
        'Content-Type': 'application/json'
      }
    };

    const response = await api.put(`/ordemdeservico/${osId}`, dadosOS, config);
    return response.data;
  } catch (error: any) {
    if (error.response?.data) {
      error.apiError = error.response.data as RespostaErrorJson;
    }
    throw error;
  }
};