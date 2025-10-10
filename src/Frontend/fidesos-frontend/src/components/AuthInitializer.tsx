// @/components/AuthInitializer.tsx
"use client";

import { useAuthStore } from "@/store/authStore";
import { useEffect } from "react";

export function AuthInitializer() {
	const { loadUser, isAuthenticated } = useAuthStore();

	useEffect(() => {
		// Carrega o usuário automaticamente quando o app inicia
		loadUser();
	}, [loadUser]);

	return null; // Este componente não renderiza nada
}
