"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
// import { useAuthStore } from "@/stores/auth-store";
import { useApiMutation } from "@/hooks/use-api-mutation";
import { useState } from "react";
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
import {
  ChangePasswordFormValues,
  changePasswordSchema,
} from "../schemas/changePasswordSchema";
import { changePassword } from "../services/profileService";

export default function ChangePasswordForm() {
  const [apiErrors, setApiErrors] = useState<string[]>([]);

  const form = useForm<ChangePasswordFormValues>({
    resolver: zodResolver(changePasswordSchema),
    defaultValues: { senha: "", novaSenha: "" },
  });

  const { mutate, isPending } = useApiMutation({
    mutationFn: changePassword,
    onSuccess: () => {
      toast.success("Senha alterada com sucesso!");
      form.reset();
    },
    onError: (errors) => {
      if (errors.length === 1) {
        toast.error("Erro ao alterar a senha", { description: errors[0] });
      } else {
        setApiErrors(errors);
      }
    },
  });

  const onSubmit = (data: ChangePasswordFormValues) => {
    mutate(data);
  };

  return (
    <>
      <Card>
        <CardHeader>
          <CardTitle>Segurança</CardTitle>
          <CardDescription>Altere sua senha de acesso.</CardDescription>
        </CardHeader>
        <CardContent>
          <Form
            form={form}
            onSubmit={form.handleSubmit(onSubmit)}
            className="space-y-4"
          >
            <FormField
              control={form.control}
              name="senha"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Senha Atual</FormLabel>
                  <FormControl>
                    <Input type="password" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="novaSenha"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Nova Senha</FormLabel>
                  <FormControl>
                    <Input type="password" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <Button type="submit" disabled={isPending}>
              {isPending ? "Salvando..." : "Alterar Senha"}
            </Button>
          </Form>
        </CardContent>
      </Card>

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
