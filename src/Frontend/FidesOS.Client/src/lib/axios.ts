import axios from "axios";
import https from "https";
import Cookies from "js-cookie";

const agent = new https.Agent({
  rejectUnauthorized: process.env.NODE_ENV === "production",
});

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
  httpsAgent: agent
});

api.interceptors.request.use(
  (config) => {
    const isClient = typeof window !== 'undefined';

    if (isClient) {
      const token = Cookies.get('accessToken');

     // console.log(`[Client Interceptor] O token do cookie do navegador é: ${token ? token.substring(0, 15) + '...' : 'NENHUM TOKEN'}`);

      if (token && token !== 'undefined' && token !== 'null') {
        config.headers.Authorization = `Bearer ${token}`;
      }

      const language = navigator.language;
      if (language) {
        config.headers['Accept-Language'] = language;
      }
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      if (typeof window !== 'undefined') {
        Cookies.remove('accessToken');
        window.location.href = '/login';
      }
    }
    return Promise.reject(error);
  }
);

export default api;