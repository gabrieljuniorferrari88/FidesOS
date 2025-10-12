import api from '@/lib/axios';
import { User } from '@/stores/auth-store';

// Tipos para as requisições (poderiam ir para uma pasta 'types' ou 'contracts')
interface LoginRequest {
  email: string;
  senha: string;
}

interface LoginResponse {
  nome: string;
  tokens: {
    accessToken: string;
  };
}

// Serviço de Login
export const loginService = async (data: LoginRequest): Promise<LoginResponse> => {
  const response = await api.post<LoginResponse>('/login', data);
  return response.data;
};

// Serviço para buscar o perfil do usuário logado
export const getProfile = async (): Promise<User> => {
    const response = await api.get<User>('/usuario'); // Ajuste o endpoint se necessário
    return response.data;
}