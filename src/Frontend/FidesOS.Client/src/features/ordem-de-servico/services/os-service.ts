import api from '@/lib/axios';
import { RespostaErrorJson } from '@/types/api-errors';
import { ListarOSParams, RequisicaoAlterarOrdemDeServicoJson, RequisicaoOrdemDeServicoJson } from '@/types/api-requisicao';
import {
  RespostaOrdemDeServicoDetalhadaJson,
  RespostaOrdemDeServicoJson,
  RespostaOrdemDeServicoResumidaJson,
  RespostaPaginadaJson
} from '@/types/api-resposta';

export const listarOrdensDeServico = async (params: ListarOSParams, token?: string) => {
  try {
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
  } catch (error: any) {
    if (error.response?.data) {
      error.apiError = error.response.data as RespostaErrorJson;
    }
    throw error;
  }
};

export const buscarOrdemServicoPorId = async (osId: string, token?: string) => {
  try {
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
  } catch (error: any) {
    if (error.response?.data) {
      error.apiError = error.response.data as RespostaErrorJson;
    }
    throw error;
  }
};

export const cancelarOrdemDeServico = async (osId: string, token?: string) => {
  try {
    const config = token ? {
      headers: {
        Authorization: `Bearer ${token}`
      }
    } : {
      headers: {
        Authorization: ""
      }
    };

    await api.delete(`/ordemdeservico/${osId}`, config);

  } catch (error: any) {
    if (error.response?.data) {
      error.apiError = error.response.data as RespostaErrorJson;
    }
    throw error;
  }
};

export const criarOrdemServico = async (dadosOS: RequisicaoOrdemDeServicoJson, token?: string) => {
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
    if (error.response?.data) {
      error.apiError = error.response.data as RespostaErrorJson;
    }
    throw error;
  }
};

export const alterarOrdemServico = async (osId: string, dadosOS: RequisicaoAlterarOrdemDeServicoJson, token?: string) => {
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