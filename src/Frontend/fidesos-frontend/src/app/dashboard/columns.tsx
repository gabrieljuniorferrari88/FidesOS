"use client";

import { RespostaOrdemDeServicoResumidaJson } from "@/api/contracts/responses";
import { cancelarOrdemDeServico } from "@/api/services/ordemDeServicoService";
import { ErrorList } from "@/components/shared/ErrorList";
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
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { useApiMutation } from "@/hooks/useApiMutation";
import { useQueryClient } from "@tanstack/react-query";
import { ColumnDef, Row } from "@tanstack/react-table";
import { format } from "date-fns";
import { ptBR } from "date-fns/locale";
import { MoreHorizontal } from "lucide-react";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { toast } from "sonner";

// 2. Crie um componente para a célula de ações
const ActionsCell = ({
	row,
}: {
	row: Row<RespostaOrdemDeServicoResumidaJson>;
}) => {
	const router = useRouter();
	const os = row.original;
	const queryClient = useQueryClient();
	const [apiErrors, setApiErrors] = useState<string[]>([]);

	const { mutate: cancelarMutate, isPending } = useApiMutation({
		mutationFn: cancelarOrdemDeServico,
		onSuccess: () => {
			toast.success("Sucesso!", {
				description: "Ordem de Serviço cancelada com sucesso!",
			});

			queryClient.invalidateQueries({ queryKey: ["ordensDeServico"] });
		},
		onError: (errors: any) => {
			if (errors.length === 1) {
				// Se for só 1 erro, usamos o toast
				toast.error("Falha ao cancelar a OS.", {
					description: errors[0],
				});
			} else {
				// Se forem múltiplos, ativamos o estado que abre o AlertDialog
				setApiErrors(errors);
			}
		},
	});

	const handleViewDetails = () => {
		router.push(`/dashboard/ordem-de-servico/${os.id}`);
	};

	return (
		<>
			<DropdownMenu>
				<DropdownMenuTrigger asChild>
					<Button
						variant="ghost"
						className="h-8 w-8 p-0"
						onClick={(e) => e.stopPropagation()}
					>
						<span className="sr-only">Abrir menu</span>
						<MoreHorizontal className="h-4 w-4" />
					</Button>
				</DropdownMenuTrigger>
				<DropdownMenuContent
					align="end"
					onClick={(e) => e.stopPropagation()}
				>
					<DropdownMenuLabel>Ações</DropdownMenuLabel>
					<DropdownMenuItem
						onClick={() => navigator.clipboard.writeText(os.id)}
						className="cursor-pointer"
					>
						Copiar ID da OS
					</DropdownMenuItem>
					{/* 3. Adicione o onClick ao item do menu */}
					<DropdownMenuItem
						onClick={handleViewDetails}
						className="cursor-pointer"
					>
						Ver Detalhes
					</DropdownMenuItem>
					<AlertDialog>
						<AlertDialogTrigger asChild>
							<DropdownMenuItem
								className="cursor-pointer text-red-500 focus:text-red-600"
								// Impede que o menu se feche ao clicar
								onSelect={(e) => e.preventDefault()}
							>
								Cancelar OS
							</DropdownMenuItem>
						</AlertDialogTrigger>
						<AlertDialogContent>
							<AlertDialogHeader>
								<AlertDialogTitle>
									Você tem certeza?
								</AlertDialogTitle>
								<AlertDialogDescription>
									Esta ação não pode ser desfeita. A Ordem de
									Serviço será marcada como "Cancelada".
								</AlertDialogDescription>
							</AlertDialogHeader>
							<AlertDialogFooter>
								<AlertDialogCancel>Voltar</AlertDialogCancel>
								<AlertDialogAction
									disabled={isPending}
									onClick={() => cancelarMutate(os.id)}
								>
									{isPending
										? "Cancelando..."
										: "Sim, cancelar OS"}
								</AlertDialogAction>
							</AlertDialogFooter>
						</AlertDialogContent>
					</AlertDialog>
				</DropdownMenuContent>
			</DropdownMenu>

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

// Helper para mapear o Status para um texto e uma cor
const getStatusInfo = (status: number) => {
	switch (status) {
		case 0:
			return { text: "Pendente", variant: "default" as const };
		case 1:
			return { text: "Agendada", variant: "secondary" as const };
		case 2:
			return { text: "Em Andamento", variant: "outline" as const };
		case 3:
			return { text: "Concluída", variant: "destructive" as const }; // Apenas exemplo de cor
		case 4:
			return { text: "Cancelada", variant: "destructive" as const };
		default:
			return { text: "Desconhecido", variant: "default" as const };
	}
};

// Este é o "mapa" da nossa tabela. Cada objeto representa uma coluna.
export const columns: ColumnDef<RespostaOrdemDeServicoResumidaJson>[] = [
	{
		accessorKey: "status",
		header: "Status",
		cell: ({ row }) => {
			const statusInfo = getStatusInfo(row.original.status);
			return (
				<Badge variant={statusInfo.variant}>{statusInfo.text}</Badge>
			);
		},
	},
	{
		accessorKey: "descricao",
		header: "Descrição",
	},
	{
		accessorKey: "dataAgendamento",
		header: "Agendamento",
		cell: ({ row }) => {
			const data = new Date(row.original.dataAgendamento);
			return format(data, "dd/MM/yyyy HH:mm", { locale: ptBR });
		},
	},
	{
		id: "actions",
		header: "Ações",
		cell: ActionsCell,
	},
];
