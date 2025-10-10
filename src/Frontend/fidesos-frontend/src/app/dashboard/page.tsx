"use client";

import { useQuery } from "@tanstack/react-query";
import {
	flexRender,
	getCoreRowModel,
	useReactTable,
	type PaginationState,
} from "@tanstack/react-table";
import { useState } from "react";

import { listarOrdensDeServico } from "@/api/services";
import { Button } from "@/components/ui/button";
import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow,
} from "@/components/ui/table";
import { useRouter } from "next/navigation";
import { columns } from "./columns";

export default function DashboardPage() {
	// 1. Estado para controlar a paginação (página atual e itens por página)
	const [{ pageIndex, pageSize }, setPagination] = useState<PaginationState>({
		pageIndex: 0, // TanStack Table começa o índice da página em 0
		pageSize: 10,
	});
	const router = useRouter();

	// 2. Hook do TanStack Query para buscar os dados da nossa API
	const { data, isPending, isError } = useQuery({
		// A chave da query é única para cada página, garantindo que a busca seja refeita
		queryKey: ["ordensDeServico", pageIndex, pageSize],
		// A função que será chamada para buscar os dados
		queryFn: () => listarOrdensDeServico(pageIndex + 1, pageSize), // Nossa API espera a página começando em 1
		// keepPreviousData: true, // Opcional: mantém os dados antigos visíveis enquanto os novos carregam
	});

	// 3. Hook da TanStack Table, o "cérebro" da nossa tabela
	const table = useReactTable({
		data: data?.itens ?? [], // Os dados da tabela vêm da resposta da API
		columns, // A definição das colunas que criamos no arquivo `columns.tsx`
		pageCount: data?.totalDePaginas ?? -1, // O total de páginas, vindo da API
		state: {
			pagination: { pageIndex, pageSize },
		},
		onPaginationChange: setPagination, // Conecta a mudança de página da tabela com nosso estado
		getCoreRowModel: getCoreRowModel(),
		manualPagination: true, // ESSENCIAL: Avisa à tabela que a paginação é feita no servidor
	});

	// Renderiza um estado de carregamento
	if (isPending) {
		return <div className="p-8">Carregando Ordens de Serviço...</div>;
	}

	// Renderiza um estado de erro
	if (isError) {
		return (
			<div className="p-8 text-red-500">
				Ocorreu um erro ao buscar os dados.
			</div>
		);
	}

	return (
		<div className="p-8">
			<h1 className="text-3xl font-bold">
				Dashboard de Ordens de Serviço
			</h1>

			{/* 4. Renderização da Tabela */}
			<div className="rounded-md border mt-4">
				<Table>
					<TableHeader>
						{table.getHeaderGroups().map((headerGroup) => (
							<TableRow key={headerGroup.id}>
								{headerGroup.headers.map((header) => (
									<TableHead key={header.id}>
										{flexRender(
											header.column.columnDef.header,
											header.getContext()
										)}
									</TableHead>
								))}
							</TableRow>
						))}
					</TableHeader>
					<TableBody>
						{table.getRowModel().rows?.length ? (
							table.getRowModel().rows.map((row) => (
								<TableRow
									key={row.id}
									className="cursor-pointer"
									// <<< 3. Adicione a classe para o cursor
									// 4. Adicione a função de clique para navegar
									onClick={() =>
										router.push(
											`/dashboard/ordem-de-servico/${row.original.id}`
										)
									}
								>
									{row.getVisibleCells().map((cell) => (
										<TableCell key={cell.id}>
											{flexRender(
												cell.column.columnDef.cell,
												cell.getContext()
											)}
										</TableCell>
									))}
								</TableRow>
							))
						) : (
							<TableRow>
								<TableCell
									colSpan={columns.length}
									className="h-24 text-center"
								>
									Nenhum resultado.
								</TableCell>
							</TableRow>
						)}
					</TableBody>
				</Table>
			</div>

			{/* 5. Renderização dos Controles de Paginação */}
			<div className="flex items-center justify-between py-4">
				<div className="flex-1 text-sm text-muted-foreground">
					Página {table.getState().pagination.pageIndex + 1} de{" "}
					{table.getPageCount()}
				</div>
				<div className="flex items-center space-x-2">
					<Button
						variant="outline"
						size="sm"
						onClick={() => table.previousPage()}
						disabled={!table.getCanPreviousPage()}
					>
						Anterior
					</Button>
					<Button
						variant="outline"
						size="sm"
						onClick={() => table.nextPage()}
						disabled={!table.getCanNextPage()}
					>
						Próximo
					</Button>
				</div>
			</div>
		</div>
	);
}
