"use client";

import { useMutation, type UseMutationOptions } from "@tanstack/react-query";
import { AxiosError } from "axios";

interface RespostaErrorJson {
    errors: string[];
}

// A definição de tipo para nosso onError customizado
type ApiMutationOptions<TData, TError, TVariables> = UseMutationOptions<TData, TError, TVariables> & {
    onError?: (errors: string[]) => void;
};

export const useApiMutation = <TData = unknown, TError = unknown, TVariables = void>(
    options: ApiMutationOptions<TData, TError, TVariables>
) => {
    return useMutation({
        mutationFn: options.mutationFn,
        onSuccess: options.onSuccess,

        onError: (error: TError) => {
            let errorMessages = ["Ocorreu um erro inesperado."];

            if (error instanceof AxiosError && error.response?.data) {
                const apiError = error.response.data as RespostaErrorJson;
                if (apiError.errors && apiError.errors.length > 0) {
                    errorMessages = apiError.errors;
                }
            }

            // Em vez de mostrar o toast, chamamos o onError que o componente nos passou
            if (options.onError) {
                options.onError(errorMessages);
            }
        },
    });
};
