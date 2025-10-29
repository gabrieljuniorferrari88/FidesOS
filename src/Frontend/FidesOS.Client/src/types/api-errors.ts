// types/api-errors.ts

export interface RespostaErrorJson {
  errors: string[];
  tokenIsExpired: boolean;
}

// Tipo para erros de validação de formulário (se sua API usar)
export interface ValidationError {
  field: string;
  message: string;
}

export interface RespostaValidationErrorJson {
  errors: ValidationError[];
  tokenIsExpired: boolean;
}

// Tipo union para todos os possíveis erros da API
export type ApiError = RespostaErrorJson | RespostaValidationErrorJson;

// Helper para verificar o tipo de erro
export const isTokenExpiredError = (error: any): boolean => {
  return error?.tokenIsExpired === true;
};

export const hasValidationErrors = (error: any): error is RespostaValidationErrorJson => {
  return Array.isArray(error?.errors) && error.errors[0]?.field !== undefined;
};