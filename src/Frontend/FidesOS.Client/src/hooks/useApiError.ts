import { useAuthStore } from "@/stores/auth-store";
import { isTokenExpiredError } from "@/types/api-errors";
import { toast } from "sonner";
import { useErrorAlert } from "./useErrorAlert";

export const useApiError = () => {
  const { logout } = useAuthStore();
  const { showError, errorAlert, closeError } = useErrorAlert();

  const handleError = (error: any): void => {
    if (error?.tokenIsExpired || isTokenExpiredError(error.response?.data)) {
      toast.error("Sessão expirada", {
        description: "Por favor, faça login novamente.",
      });
      logout();
      return;
    }

    showError(error);
  };

  const getErrorMessage = (error: any): string => {
    if (error?.tokenIsExpired || isTokenExpiredError(error.response?.data)) {
      return "Sessão expirada. Faça login novamente.";
    }

    const apiError = error.response?.data;
    if (apiError?.errors && apiError.errors.length > 0) {
      return apiError.errors[0];
    }

    return error.message || "Erro interno do sistema. Tente novamente.";
  };

  return {
    handleError,
    getErrorMessage,
    errorAlert,
    closeError,
  };
};