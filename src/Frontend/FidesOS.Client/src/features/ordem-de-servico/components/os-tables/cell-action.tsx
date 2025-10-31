"use client";
import {
  IconAlertTriangle,
  IconDotsVertical,
  IconEdit,
  IconFileText,
  IconTrash,
} from "@tabler/icons-react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Row } from "@tanstack/react-table";
import { useRouter } from "next/navigation";

import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";

import { SimpleWarning } from "@/components/simple-warning";
import { ErrorAlert } from "@/components/ui/error-alert";
import { useApiError } from "@/hooks/useApiError";
import { useAuthStore } from "@/stores/auth-store";
import { ApiError } from "@/types/api-errors";
import { RespostaOrdemDeServicoResumidaJson } from "@/types/api-resposta";
import { StatusOS } from "@/types/enums";
import { toast } from "sonner";
import { cancelarOrdemDeServico } from "../../services/os-service";

interface CellActionProps {
  row: Row<RespostaOrdemDeServicoResumidaJson>; // <<< Use o tipo correto
}

export const CellAction: React.FC<CellActionProps> = ({ row }) => {
  const os = row.original;
  const router = useRouter();
  const queryClient = useQueryClient();
  const { token, isAuthenticated } = useAuthStore();
  const { handleError, errorAlert, closeError } = useApiError();

  const { mutate, isPending } = useMutation({
    mutationFn: async (id: string) => {
      if (!token || !isAuthenticated) {
        throw new Error("Usuário não autenticado");
      }

      return await cancelarOrdemDeServico(id, token!);
    },
    onSuccess: () => {
      toast.success("Ordem de Serviço cancelada com sucesso!");
      router.refresh();
      queryClient.invalidateQueries({ queryKey: ["ordensDeServico"] });
    },
    onError: (errors: ApiError) => {
      const errorMessage = errors || "Erro ao cancelar OS";
      handleError(errorMessage);
    },
  });

  const handleViewDetails = () => {
    router.push(`/dashboard/ordem-de-servico/detalhes/${os.id}`);
  };

  const handleCancel = (id: string) => mutate(id);

  // const handleCancelClick = () => {
  //   if (os.status === StatusOS.Cancelada) {
  //     toast.info("Ordem de Serviço já cancelada!");
  //     return false; // Impede que o modal abra
  //   }
  //   return true; // Permite que o modal abra
  // };

  return (
    <>
      <DropdownMenu modal={false}>
        <DropdownMenuTrigger asChild>
          <Button variant="ghost" className="h-8 w-8 p-0">
            <span className="sr-only">Open menu</span>
            <IconDotsVertical className="h-4 w-4" />
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end" onClick={(e) => e.stopPropagation()}>
          <DropdownMenuLabel>Ações</DropdownMenuLabel>
          <DropdownMenuItem onClick={handleViewDetails}>
            <IconFileText className="mr-2 h-4 w-4" /> Ver Detalhes
          </DropdownMenuItem>
          {os.status !== StatusOS.Cancelada ? (
            <DropdownMenuItem
              onClick={() =>
                router.push(`/dashboard/ordem-de-servico/${os.id}`)
              }
            >
              <IconEdit className="mr-2 h-4 w-4" /> Editar OS
            </DropdownMenuItem>
          ) : (
            <DropdownMenuItem
              onClick={() =>
                toast.warning(
                  "Ordem de Serviço cancelada, impossível alteração!"
                )
              }
            >
              <IconEdit className="mr-2 h-4 w-4" /> Editar OS
            </DropdownMenuItem>
          )}
          <DropdownMenuSeparator />

          <AlertDialog>
            <AlertDialogTrigger asChild>
              <DropdownMenuItem
                className="cursor-pointer text-red-500 focus:text-red-600"
                onSelect={(e) => {
                  e.preventDefault();
                  if (os.status === StatusOS.Cancelada) {
                    toast.warning("Ordem de Serviço já cancelada!");
                    return;
                  }
                }}
              >
                <IconTrash className="mr-2 h-4 w-4" /> Cancelar OS
              </DropdownMenuItem>
            </AlertDialogTrigger>
            {os.status !== StatusOS.Cancelada && (
              <AlertDialogContent>
                <AlertDialogHeader>
                  <AlertDialogTitle className="text-red-500 flex items-center">
                    <IconAlertTriangle className="mr-2 h-4 w-4" />
                    Cancelar
                  </AlertDialogTitle>
                  <AlertDialogDescription>
                    Tem certeza de que deseja desativar esta{" "}
                    <span className="font-bold">Ordem de Serviço?</span>
                  </AlertDialogDescription>
                  <AlertDialogDescription>
                    Esta ação <span className="font-bold">desativará</span> a
                    Ordem de Serviço não podendo altera-la mais tarde. Proceda
                    com cautela.
                  </AlertDialogDescription>

                  <SimpleWarning
                    title="Atenção!"
                    description="Esta ação não pode ser desfeita."
                  />
                </AlertDialogHeader>
                <AlertDialogFooter>
                  <AlertDialogCancel>Voltar</AlertDialogCancel>
                  <AlertDialogAction
                    className="cursor-pointer"
                    disabled={isPending}
                    onClick={() => handleCancel(os.id)}
                  >
                    {isPending ? "Cancelando..." : "Sim, cancelar OS"}
                  </AlertDialogAction>
                </AlertDialogFooter>
              </AlertDialogContent>
            )}
          </AlertDialog>
        </DropdownMenuContent>
      </DropdownMenu>

      <AlertDialog
        open={errorAlert.errors.length > 0}
        onOpenChange={(open) => !open && closeError()}
      >
        <AlertDialogContent className="max-w-md">
          <AlertDialogHeader className="py-2"></AlertDialogHeader>
          <div className="py-2">
            <ErrorAlert
              title="Erro ao cancelar OS"
              errors={errorAlert.errors}
              onClose={closeError}
            />
          </div>

          <AlertDialogFooter>
            <AlertDialogCancel onClick={closeError}>Fechar</AlertDialogCancel>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </>
  );
};
