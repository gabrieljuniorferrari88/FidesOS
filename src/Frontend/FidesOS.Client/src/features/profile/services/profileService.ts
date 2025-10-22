import api from '@/lib/axios';
import { User } from '@/stores/auth-store';
import { ChangePasswordFormValues } from '../schemas/changePasswordSchema';
import { UpdateProfileFormValues } from '../schemas/updateProfileSchema';

// O tipo de resposta do nosso endpoint de perfil (igual ao da auth-store)
interface ProfileResponse extends User {}

/**
 * Chama o endpoint de ATUALIZAR PERFIL (PUT /usuario).
 * O backend identifica o usuário pelo token.
 */
export const updateProfile = async (data: UpdateProfileFormValues) => {
  // O endpoint PUT não precisa retornar um corpo, apenas um status 204
  await api.put('/usuario', data);
};

/**
 * Chama o endpoint de ALTERAR SENHA (PUT /usuario/alterar-senha).
 */
export const changePassword = async (data: ChangePasswordFormValues) => {
  await api.put('/usuario/alterar-senha', data);
};

/**
 * Chama o endpoint de BUSCAR PERFIL (GET /usuario).
 * (Estamos movendo o 'getProfile' da auth-service para cá,
 * pois ele pertence à feature 'profile')
 */
export const getProfile = async (): Promise<ProfileResponse> => {
    const response = await api.get<ProfileResponse>('/usuario'); // Ajuste da rota para /usuario
    return response.data;
}