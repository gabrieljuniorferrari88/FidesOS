"use client";

import { useMutation, type UseMutationOptions } from "@tanstack/react-query";
import { AxiosError } from "axios";

interface RespostaErrorJson {
    errors: string[];
}

type ApiMutationOptions<TData, TError, TVariables, TContext> = 
    Omit<UseMutationOptions<TData, TError, TVariables, TContext>, 'onError'> & {
    onError?: (errors: string[]) => void;
};

export function useApiMutation<
    TData = unknown, 
    TError = unknown, 
    TVariables = void, 
    TContext = unknown
>(
    options: ApiMutationOptions<TData, TError, TVariables, TContext>
) {
    const { onError: onCustomError, ...restOptions } = options;

    return useMutation({
        ...restOptions,

        onError: (error: TError, variables: TVariables, context: TContext | undefined) => {
            let errorMessages = ["Ocorreu um erro inesperado."];

            if (error instanceof AxiosError && error.response?.data) {
                const apiError = error.response.data as RespostaErrorJson;
                if (apiError.errors && apiError.errors.length > 0) {
                    errorMessages = apiError.errors;
                }
            }

            if (onCustomError) {
                onCustomError(errorMessages);
            }
        },
    });
};