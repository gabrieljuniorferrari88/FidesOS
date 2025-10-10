"use client";

import { RespostaOrdemDeServicoResumidaJson } from "@/api/contracts/responses";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { ColumnDef, Row } from "@tanstack/react-table";
import { format } from "date-fns";
import { ptBR } from "date-fns/locale";
import { MoreHorizontal } from "lucide-react";
import { useRouter } from "next/navigation";

// 2. Crie um componente para a célula de ações
const ActionsCell = ({
	row,
}: {
	row: Row<RespostaOrdemDeServicoResumidaJson>;
}) => {
	const router = useRouter();
	const os = row.original;

	const handleViewDetails = () => {
		router.push(`/dashboard/ordem-de-servico/${os.id}`);
	};

	return (
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
				<DropdownMenuItem className="cursor-pointer text-red-500 focus:text-red-600">
					Cancelar OS
				</DropdownMenuItem>
			</DropdownMenuContent>
		</DropdownMenu>
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
