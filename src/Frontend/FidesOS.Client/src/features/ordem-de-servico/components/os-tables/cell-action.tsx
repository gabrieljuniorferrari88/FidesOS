"use client";
import { useRouter } from "next/navigation";
import { useState } from "react";
//import { toast } from 'sonner';
import {
  IconDotsVertical,
  IconEdit,
  IconFileText,
  IconTrash,
} from "@tabler/icons-react";
import { useQueryClient } from "@tanstack/react-query";
import { Row } from "@tanstack/react-table";

//import { AlertModal } from '@/components/modal/alert-modal';
import { ErrorList } from "@/components/shared/ErrorList"; // Assumindo que você criou este
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

import { RespostaOrdemDeServicoResumidaJson } from "@/types/api-resposta"; // <<< Use o seu tipo
//import { useApiMutation } from '@/hooks/use-api-mutation'; // <<< Use nosso hook
//import { cancelarOrdemDeServico } from '@/features/ordem-de-servico/services/os-service'; // <<< Use nosso serviço

interface CellActionProps {
  row: Row<RespostaOrdemDeServicoResumidaJson>; // <<< Use o tipo correto
}

export const CellAction: React.FC<CellActionProps> = ({ row }) => {
  const os = row.original;
  const router = useRouter();
  const queryClient = useQueryClient();

  // Nosso estado para controlar o diálogo de múltiplos erros
  const [apiErrors, setApiErrors] = useState<string[]>([]);

  // Nossa mutação para cancelar a OS, usando o hook customizado
  // const { mutate: cancelarMutate, isPending: isCanceling } = useApiMutation({
  //   mutationFn: cancelarOrdemDeServico,
  //   onSuccess: () => {
  //       toast.success("Ordem de Serviço cancelada com sucesso!");
  //       queryClient.invalidateQueries({ queryKey: ['ordensDeServico'] });
  //   },
  //   onError: (errors) => {
  //       if (errors.length === 1) {
  //           toast.error("Erro ao cancelar", { description: errors[0] });
  //       } else {
  //           setApiErrors(errors);
  //       }
  //   }
  // });

  const handleViewDetails = () => {
    router.push(`/dashboard/ordem-de-servico/${os.id}`);
  };

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
          <DropdownMenuItem
            onClick={() => router.push(`/dashboard/ordem-de-servico/${os.id}`)}
          >
            <IconEdit className="mr-2 h-4 w-4" /> Editar OS
          </DropdownMenuItem>
          <DropdownMenuSeparator />

          {/* Ação de Cancelar com AlertDialog de Confirmação */}
          <AlertDialog>
            <AlertDialogTrigger asChild>
              <DropdownMenuItem
                className="cursor-pointer text-red-500 focus:text-red-600"
                onSelect={(e) => e.preventDefault()}
              >
                <IconTrash className="mr-2 h-4 w-4" /> Cancelar OS
              </DropdownMenuItem>
            </AlertDialogTrigger>
            <AlertDialogContent>
              <AlertDialogHeader>
                <AlertDialogTitle>Você tem certeza?</AlertDialogTitle>
                <AlertDialogDescription>
                  A OS será marcada como "Cancelada". Esta ação não pode ser
                  desfeita.
                </AlertDialogDescription>
              </AlertDialogHeader>
              <AlertDialogFooter>
                <AlertDialogCancel>Voltar</AlertDialogCancel>
                {/* <AlertDialogAction
                  disabled={isCanceling}
                  onClick={() => cancelarMutate(os.id)}
                >
                  {isCanceling ? "Cancelando..." : "Sim, cancelar OS"}
                </AlertDialogAction> */}
              </AlertDialogFooter>
            </AlertDialogContent>
          </AlertDialog>
        </DropdownMenuContent>
      </DropdownMenu>

      {/* AlertDialog para exibir múltiplos erros da API */}
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
};
