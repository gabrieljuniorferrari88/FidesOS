import { useAuthStore } from "@/stores/auth-store";
import { RespostaErrorJson, hasValidationErrors, isTokenExpiredError } from "@/types/api-errors";
import { useState } from "react";

export interface ErrorAlertState {
  isOpen: boolean;
  title: string;
  errors: string[];
}

export const useErrorAlert = () => {
  const [errorAlert, setErrorAlert] = useState<ErrorAlertState>({
    isOpen: false,
    title: "",
    errors: [],
  });

  const { logout } = useAuthStore();

  const showError = (error: any): void => {
    console.error("API Error:", error);

    // Token expirado - comportamento especial
    if (error?.tokenIsExpired || isTokenExpiredError(error.response?.data)) {
      logout();
      // Podemos mostrar um toast rápido antes do logout
      // toast.error("Sessão expirada. Redirecionando...");
      return;
    }

    // Erro de validação
    if (hasValidationErrors(error.response?.data)) {
      const validationErrors = error.response.data.errors;
      const errorMessages = validationErrors.map((err: any) => 
        `${err.field}: ${err.message}`
      );
      
      setErrorAlert({
        isOpen: true,
        title: "Erros de Validação",
        errors: errorMessages,
      });
      return;
    }

    // Erro padrão da API
    const apiError = error.response?.data as RespostaErrorJson;
    if (apiError?.errors && apiError.errors.length > 0) {
      setErrorAlert({
        isOpen: true,
        title: apiError.errors.length > 1 ? "Foram encontrados os seguintes erros:" : "Erro",
        errors: apiError.errors,
      });
      return;
    }

    // Erro genérico
    setErrorAlert({
      isOpen: true,
      title: "Erro",
      errors: [error.message || "Erro interno do sistema. Tente novamente."],
    });
  };

  const closeError = () => {
    setErrorAlert({
      isOpen: false,
      title: "",
      errors: [],
    });
  };

  return {
    errorAlert,
    showError,
    closeError,
  };
};