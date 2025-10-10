import { getProfile } from '@/api/services/usuarioService';
import Cookies from 'js-cookie';
import { create } from 'zustand';

interface User {
  nome: string;
  email: string;
}

interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean; // ← ADICIONEI ESTADO DE LOADING
  login: (token: string) => Promise<void>;
  logout: () => void;
  loadUser: () => Promise<void>; // ← ADICIONEI MÉTODO PARA CARREGAR USUÁRIO
}

export const useAuthStore = create<AuthState>()((set, get) => ({
  user: null,
  isAuthenticated: !!Cookies.get('accessToken'),
  isLoading: true, // ← COMEÇA COMO TRUE

  // Método para carregar o usuário quando o token existe
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
      set({
        isAuthenticated: true,
        user: userProfile,
        isLoading: false
      });
    } catch (error) {
      console.error("Falha ao buscar perfil após o login.", error);
      set({
        isAuthenticated: true,
        user: null,
        isLoading: false
      });
    }
  },

  logout: () => {
    Cookies.remove('accessToken');
    set({
      isAuthenticated: false,
      user: null,
      isLoading: false
    });
  },
}));
