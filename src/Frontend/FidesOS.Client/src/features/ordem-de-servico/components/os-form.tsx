"use client";

import { FormDateTimePicker } from "@/components/forms/form-datetime-picker";
import { FormInput } from "@/components/forms/form-input";
import { FormTextarea } from "@/components/forms/form-textarea";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { ErrorAlert } from "@/components/ui/error-alert";
import { Form } from "@/components/ui/form";

import { useApiError } from "@/hooks/useApiError";
import { useAuthStore } from "@/stores/auth-store";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";

import { RequisicaoOrdemDeServicoJson } from "@/types/api-requisicao";
import {
  RespostaOrdemDeServicoDetalhadaJson,
  RespostaOrdemDeServicoJson,
} from "@/types/api-resposta";
import { criarOrdemServico } from "../services/os-service";

import { toast } from "sonner";
import * as z from "zod";

const formSchema = z.object({
  empresaClientId: z
    .string()
    .nonempty({ message: "Empresa cliente é obrigatório" }),
  dataAgendamento: z.date({
    error: "A data e hora de agendamento é obrigatória.",
  }),
  descricao: z.string().min(10, {
    message: "A descrição deve ter pelo menos 10 caracteres.",
  }),
});

export default function OrdemDeServicoForm({
  initialData,
  pageTitle,
}: {
  initialData:
    | RequisicaoOrdemDeServicoJson
    | RespostaOrdemDeServicoDetalhadaJson
    | null;
  pageTitle: string;
}) {
  const defaultValues = {
    empresaClientId: initialData?.empresaClientId || "",
    dataAgendamento: initialData?.dataAgendamento
      ? new Date(initialData.dataAgendamento)
      : undefined,
    descricao: initialData?.descricao || "",
  };

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: defaultValues,
  });

  const router = useRouter();
  const { token, isAuthenticated, logout } = useAuthStore();

  const { handleError, errorAlert, closeError } = useApiError();

  const { mutate, isPending } = useMutation({
    mutationFn: async (values: z.infer<typeof formSchema>) => {
      // Verificação de segurança
      if (!token || !isAuthenticated) {
        handleError("Usuário não autenticado");
      }

      return await criarOrdemServico(
        {
          empresaClienteId: values.empresaClientId,
          descricao: values.descricao,
          dataAgendamento: values.dataAgendamento.toISOString(),
        },
        token!
      );
    },
    onSuccess: (data: RespostaOrdemDeServicoJson) => {
      toast.success("Ordem de serviço criada com sucesso!", {
        description: "A OS foi cadastrada no sistema.",
      });

      // Redireciona após sucesso
      router.push("/dashboard/ordem-de-servico");
    },
    onError: (error: any) => {
      handleError(error);
    },
  });

  const onSubmit = (values: z.infer<typeof formSchema>) => {
    mutate(values);
  };

  return (
    <Card className="mx-auto w-full">
      <CardHeader>
        <CardTitle className="text-left text-2xl font-bold">
          {pageTitle}
        </CardTitle>
      </CardHeader>
      <CardContent>
        {/* 🔥 Alert de erros múltiplos */}
        {errorAlert.isOpen && (
          <ErrorAlert
            title={errorAlert.title}
            errors={errorAlert.errors}
            onClose={closeError}
          />
        )}

        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
            <div className="grid grid-cols-1 gap-6 md:grid-cols-2">
              <FormInput
                control={form.control}
                name="empresaClientId"
                label="Empresa Cliente ID"
                placeholder="Informe o Id do cliente"
                required
              />

              <FormDateTimePicker
                // className="w-full md:max-w-[290px]"
                control={form.control}
                name="dataAgendamento"
                label="Data de Agendamento"
                required
                config={{
                  placeholder: "Escolha a data",
                  minDate: new Date(),
                }}
              />
            </div>

            <FormTextarea
              control={form.control}
              name="descricao"
              label="Descrição"
              placeholder="Informe a descrição da ordem de serviço"
              required
              config={{
                maxLength: 500,
                showCharCount: true,
                rows: 4,
              }}
            />
            <Button type="submit" className="w-full" disabled={isPending}>
              {isPending ? "Criando os..." : "Add Nova OS"}
            </Button>
          </form>
        </Form>
      </CardContent>
    </Card>
  );
}
