import { getProfile } from '@/features/auth/services/auth-service'; // Vamos criar este a seguir
import Cookies from 'js-cookie';
import { create } from 'zustand';

// A "forma" dos dados do nosso usuário
export interface User {
  nome: string;
  email: string;
  avatar?: string;
  // Adicione outros campos do seu backend aqui (avatarUrl, etc.)
}

interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (token: string) => Promise<void>;
  logout: () => void;
  loadUser: () => Promise<void>;
}

export const useAuthStore = create<AuthState>()((set, get) => ({
  user: null,
  isAuthenticated: !!Cookies.get('accessToken'),
  isLoading: true, // Começa carregando

   loadUser: async () => {
    const token = Cookies.get('accessToken');

    if (!token) {
      set({ isLoading: false, isAuthenticated: false, user: null });
      return;
    }

    try {
      const userProfile = await getProfile();
      set({
        isAuthenticated: true,
        user: userProfile,
        isLoading: false
      });
    } catch (error) {
      console.error("Falha ao carregar perfil do usuário.", error);
      // Se falhar ao carregar o perfil, faz logout
      Cookies.remove('accessToken');
      set({
        isAuthenticated: false,
        user: null,
        isLoading: false
      });
    }
  },

  login: async (token: string) => {
    Cookies.set('accessToken', token, { expires: 1, secure: true });
    try {
      const userProfile = await getProfile();
      set({ isAuthenticated: true, user: userProfile, isLoading: false });
    } catch (error) {
      console.error("Falha ao buscar perfil após login.", error);
      set({ isAuthenticated: true, user: null, isLoading: false });
    }
  },

  logout: () => {
    Cookies.remove('accessToken');
    set({ isAuthenticated: false, user: null, isLoading: false });
  }
}));