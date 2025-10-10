import axios from 'axios';
import Cookies from 'js-cookie'; // <<< Importe a biblioteca de cookies

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use(
  (config) => {
    // Lê o token diretamente do cookie
    const token = Cookies.get('accessToken');

	const language = navigator.language;

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

	if (language) {
        config.headers['Accept-Language'] = language;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api;
