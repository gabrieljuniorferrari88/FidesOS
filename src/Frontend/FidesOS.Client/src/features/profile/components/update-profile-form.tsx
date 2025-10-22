"use client";

import { useAuthStore } from "@/stores/auth-store";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { ErrorList } from "@/components/shared/ErrorList";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from "@/components/ui/alert-dialog";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { useApiMutation } from "@/hooks/use-api-mutation"; // <<< Usando nosso hook
import {
  UpdateProfileFormValues,
  updateProfileSchema,
} from "../schemas/updateProfileSchema";
import { updateProfile } from "../services/profileService";

export default function UpdateProfileForm() {
  const { user, loadUser } = useAuthStore();
  const [apiErrors, setApiErrors] = useState<string[]>([]); // Estado para o AlertDialog

  const form = useForm<UpdateProfileFormValues>({
    resolver: zodResolver(updateProfileSchema),
    defaultValues: {
      nome: user?.nome || "",
      email: user?.email || "",
    },
  });

  const { mutate, isPending } = useApiMutation({
    mutationFn: updateProfile,
    onSuccess: async () => {
      toast.success("Perfil atualizado com sucesso!");
      await loadUser(); // Recarrega os dados do usuário na store
    },
    // Nossa lógica de UI para erros:
    onError: (errors) => {
      if (errors.length === 1) {
        toast.error("Erro ao atualizar", { description: errors[0] });
      } else {
        setApiErrors(errors);
      }
    },
  });

  const onSubmit = (data: UpdateProfileFormValues) => {
    mutate(data);
  };

  return (
    <>
      <Card>
        <CardHeader>
          <CardTitle>Profile details</CardTitle>
          <CardDescription>Atualize seu nome e e-mail.</CardDescription>
        </CardHeader>
        <CardContent>
          <Form
            form={form}
            onSubmit={form.handleSubmit(onSubmit)}
            className="space-y-4"
          >
            <FormField
              control={form.control}
              name="nome"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Nome Completo</FormLabel>
                  <FormControl>
                    <Input placeholder="Seu nome" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>E-mail</FormLabel>
                  <FormControl>
                    <Input
                      type="email"
                      placeholder="seu@email.com"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <Button type="submit" disabled={isPending}>
              {isPending ? "Salvando..." : "Salvar Alterações"}
            </Button>
          </Form>
        </CardContent>
      </Card>

      {/* O AlertDialog para múltiplos erros */}
      <AlertDialog
        open={apiErrors.length > 0}
        onOpenChange={() => setApiErrors([])}
      >
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle className="text-red-500">
              Ocorreram Erros
            </AlertDialogTitle>
            <AlertDialogDescription>
              <ErrorList errors={apiErrors} />
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogAction onClick={() => setApiErrors([])}>
              Fechar
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </>
  );
}
