"use client";
//import { useTheme } from 'next-themes';
import React, { useState } from "react"; // <<< 1. Importe o useState
import { ActiveThemeProvider } from "../active-theme";

// <<< 2. Importe as ferramentas do TanStack Query
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

export default function Providers({
  activeThemeValue,
  children,
}: {
  activeThemeValue: string;
  children: React.ReactNode;
}) {
  // 3. Crie a instância do "cérebro" do QueryClient.
  // Usamos useState para garantir que ele seja criado apenas uma vez.
  const [queryClient] = useState(() => new QueryClient());

  return (
    // 4. Envolva tudo com o "capacete" do QueryClientProvider
    <QueryClientProvider client={queryClient}>
      <ActiveThemeProvider initialTheme={activeThemeValue}>
        {/* Removemos o ClerkProvider daqui */}
        {children}
      </ActiveThemeProvider>
    </QueryClientProvider>
  );
}
