import api from "@/api";
import { LoginFormInputs } from "@/api/schemas/loginSchema";

// Definimos o formato da resposta que esperamos da API de login
interface LoginResponse {
  nome: string;
  tokens: {
    accessToken: string;
  };
}

// Esta é a função que faz a chamada. Ela recebe os dados do formulário
// e os envia para o endpoint 'login' do nosso backend.
export const authService = async (data: LoginFormInputs): Promise<LoginResponse> => {
  const response = await api.post<LoginResponse>('/login', data);
  return response.data;
};
