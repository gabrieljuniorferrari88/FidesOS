"use client";

import { buscarOsDetalhada } from "@/api/services/ordemDeServicoService";
import { Badge } from "@/components/ui/badge";
import {
	Card,
	CardContent,
	CardDescription,
	CardHeader,
	CardTitle,
} from "@/components/ui/card";
import { useQuery } from "@tanstack/react-query";
import { useParams } from "next/navigation";
//import { Separator } from "@/components/ui/separator";
import { format } from "date-fns";
import { ptBR } from "date-fns/locale";

interface OrdemDeServicoDetalhePageProps {
	params: {
		osId: string;
	};
}

// Helper para converter o status em texto
const getStatusText = (status: number) => {
	const statusMap: { [key: number]: string } = {
		0: "Pendente",
		1: "Agendada",
		2: "Em Andamento",
		3: "Concluída",
		4: "Cancelada",
	};
	return statusMap[status] || "Desconhecido";
};

export default function OrdemDeServicoDetalhePage() {
	const params = useParams();
	const osId = params.osId as string;

	// 1. Usamos o useQuery para buscar os dados
	const {
		data: os,
		isPending,
		isError,
	} = useQuery({
		queryKey: ["ordemDeServicoDetalhe", osId], // Chave única para esta OS
		queryFn: () => buscarOsDetalhada(osId), // Função que chama a API
	});

	// 2. Lidamos com os estados de carregamento e erro
	if (isPending) {
		return <div className="p-8">Carregando detalhes da OS...</div>;
	}
	if (isError || !os) {
		return (
			<div className="p-8 text-red-500">
				Erro ao buscar os detalhes da OS.
			</div>
		);
	}

	// 3. Renderizamos os dados quando a busca é bem-sucedida
	return (
		<div className="p-8 space-y-6">
			<Card>
				<CardHeader>
					<div className="flex justify-between items-start">
						<div>
							<CardTitle className="text-2xl">
								Detalhes da Ordem de Serviço
							</CardTitle>
							<CardDescription>ID: {os.id}</CardDescription>
						</div>
						<Badge variant="outline">
							{getStatusText(os.status)}
						</Badge>
					</div>
				</CardHeader>
				<CardContent className="space-y-2">
					<p>
						<strong className="font-semibold">Descrição:</strong>{" "}
						{os.descricao}
					</p>
					<p>
						<strong className="font-semibold">Agendamento:</strong>{" "}
						{format(
							new Date(os.dataAgendamento),
							"dd/MM/yyyy HH:mm",
							{ locale: ptBR }
						)}
					</p>
				</CardContent>
			</Card>

			<div>
				<h2 className="text-2xl font-bold mb-4">
					Alocações de Trabalhadores
				</h2>
				<div className="space-y-4">
					{os.alocacoes.map((alocacao) => (
						<Card key={alocacao.id}>
							<CardHeader>
								<CardTitle>
									Trabalhador ID:{" "}
									{alocacao.trabalhadorIdentificacao}
								</CardTitle>
								<CardDescription>
									Valor Total:{" "}
									{(alocacao.valorTotal / 100).toLocaleString(
										"pt-BR",
										{ style: "currency", currency: "BRL" }
									)}
								</CardDescription>
							</CardHeader>
							<CardContent>
								<h3 className="font-semibold mb-2">
									Detalhes de Produção:
								</h3>
								{alocacao.detalhes.length > 0 ? (
									<ul className="list-disc pl-5 space-y-1">
										{alocacao.detalhes.map((detalhe) => (
											<li key={detalhe.id}>
												{detalhe.descricao} -{" "}
												{(
													detalhe.valor / 100
												).toLocaleString("pt-BR", {
													style: "currency",
													currency: "BRL",
												})}
											</li>
										))}
									</ul>
								) : (
									<p className="text-sm text-muted-foreground">
										Nenhum detalhe de produção adicionado.
									</p>
								)}
							</CardContent>
						</Card>
					))}
				</div>
			</div>
		</div>
	);
}
