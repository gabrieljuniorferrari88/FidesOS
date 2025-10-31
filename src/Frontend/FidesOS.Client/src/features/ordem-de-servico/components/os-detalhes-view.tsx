"use client";

import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
import { calculateTotalCents, formatCentsToReais } from "@/lib/format";
import { RespostaOrdemDeServicoDetalhadaJson } from "@/types/api-resposta";
import { StatusOS } from "@/types/enums";
import {
  IconBuilding,
  IconCalendar,
  IconCalendarEvent,
  IconCash,
  IconChartBar,
  IconClock,
  IconEdit,
  IconFileDescription,
  IconId,
  IconListDetails,
  IconRefresh,
  IconUser,
  IconUserPlus,
} from "@tabler/icons-react";
import { format, formatDistanceToNow } from "date-fns";
import { ptBR } from "date-fns/locale";

// Mapeamento completo de status com cores e ícones
const STATUS_CONFIG: Record<
  number,
  {
    label: string;
    variant: "default" | "secondary" | "destructive" | "outline";
    icon: React.ReactNode;
  }
> = {
  [StatusOS.Pendente]: {
    label: "Pendente",
    variant: "outline",
    icon: <IconClock className="h-3 w-3" />,
  },
  [StatusOS.Agendada]: {
    label: "Agendada",
    variant: "default",
    icon: <IconCalendar className="h-3 w-3" />,
  },
  [StatusOS.EmAndamento]: {
    label: "Em Andamento",
    variant: "secondary",
    icon: <IconRefresh className="h-3 w-3" />,
  },
  [StatusOS.Concluida]: {
    label: "Concluída",
    variant: "default",
    icon: <IconChartBar className="h-3 w-3" />,
  },
  [StatusOS.Cancelada]: {
    label: "Cancelada",
    variant: "destructive",
    icon: <IconClock className="h-3 w-3" />,
  },
};

interface OsDetalhesViewProps {
  initialData: RespostaOrdemDeServicoDetalhadaJson;
}

export default function OsDetalhesView({
  initialData: os,
}: OsDetalhesViewProps) {
  const statusConfig =
    STATUS_CONFIG[os.status] || STATUS_CONFIG[StatusOS.Pendente];
  const dataAgendamento = new Date(os.dataAgendamento?.toString() || "");
  const isAgendamentoFuturo = dataAgendamento > new Date();

  const totalTrabalhadores = os.alocacoes?.length || 0;
  const valorTotalOS = calculateTotalCents(
    os.alocacoes?.flatMap(
      (alocacao) =>
        alocacao.detalhes?.map((detalhe) => ({ valor: detalhe.valor })) || []
    ) || []
  );

  // Data de criação (assumindo que existe no objeto - ajuste conforme sua API)
  const dataCriacao = new Date(); // Substitua por os.dataCriacao quando disponível
  const ultimaAtualizacao = new Date(); // Substitua por os.ultimaAtualizacao quando disponível

  return (
    <div
      className="flex flex-col gap-6"
      role="main"
      aria-label="Detalhes da Ordem de Serviço"
    >
      {/* 🔥 CABEÇALHO PROFISSIONAL */}
      <header className="space-y-3">
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <div className="space-y-2">
            <h1 className="text-3xl font-bold tracking-tight text-foreground">
              Ordem de Serviço
            </h1>
            <div className="flex flex-col sm:flex-row sm:items-center gap-2 text-muted-foreground">
              <div className="flex items-center gap-2">
                <IconId className="h-4 w-4" />
                <code
                  className="text-sm bg-muted px-2 py-1 rounded"
                  aria-label={`ID da OS: ${os.id}`}
                >
                  {os.id}
                </code>
              </div>
              <Badge
                variant={statusConfig.variant}
                className="gap-1 w-fit"
                aria-label={`Status: ${statusConfig.label}`}
              >
                {statusConfig.icon}
                {statusConfig.label}
              </Badge>
            </div>
          </div>

          {/* Ações Rápidas */}
          <div className="flex gap-2">
            <Button variant="outline" size="sm" className="gap-2">
              <IconEdit className="h-4 w-4" />
              Editar
            </Button>
            <Button size="sm" className="gap-2">
              <IconUserPlus className="h-4 w-4" />
              {totalTrabalhadores > 0
                ? "Gerenciar Alocações"
                : "Alocar Trabalhador"}
            </Button>
          </div>
        </div>

        <p className="text-lg text-muted-foreground leading-relaxed max-w-3xl">
          Visualização completa dos detalhes e informações da ordem de serviço.
          {isAgendamentoFuturo && (
            <span className="text-blue-600 font-medium">
              {" "}
              Agendada para{" "}
              {format(dataAgendamento, "dd 'de' MMMM", { locale: ptBR })}.
            </span>
          )}
        </p>
      </header>

      {/* 📊 GRID PRINCIPAL */}
      <div
        className="grid grid-cols-1 gap-6 lg:grid-cols-3"
        aria-label="Conteúdo principal"
      >
        {/* 📋 COLUNA PRINCIPAL */}
        <div className="lg:col-span-2 space-y-6">
          {/* Card de Informações Principais */}
          <Card className="shadow-sm">
            <CardHeader className="pb-4">
              <CardTitle className="flex items-center gap-2 text-xl">
                <IconFileDescription className="h-5 w-5 text-primary" />
                Informações da OS
              </CardTitle>
            </CardHeader>
            <CardContent className="space-y-6">
              {/* Grid de Informações */}
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div className="space-y-1">
                  <div className="flex items-center gap-2 text-sm font-medium text-muted-foreground">
                    <IconBuilding className="h-4 w-4" />
                    Empresa Cliente
                  </div>
                  <p
                    className="text-lg font-semibold"
                    aria-label={`ID do cliente: ${os.empresaClienteId}`}
                  >
                    {os.empresaClienteId}
                  </p>
                </div>

                <div className="space-y-1">
                  <div className="flex items-center gap-2 text-sm font-medium text-muted-foreground">
                    <IconCalendarEvent className="h-4 w-4" />
                    Data de Agendamento
                  </div>
                  <div>
                    <p className="text-lg font-semibold">
                      {format(dataAgendamento, "dd/MM/yyyy 'às' HH:mm", {
                        locale: ptBR,
                      })}
                    </p>
                    <p className="text-sm text-muted-foreground">
                      {isAgendamentoFuturo
                        ? `Daqui a ${formatDistanceToNow(dataAgendamento, {
                            locale: ptBR,
                          })}`
                        : `${formatDistanceToNow(dataAgendamento, {
                            locale: ptBR,
                            addSuffix: true,
                          })}`}
                    </p>
                  </div>
                </div>
              </div>

              <Separator />

              {/* Descrição */}
              <div className="space-y-3">
                <div className="flex items-center gap-2 text-sm font-medium text-muted-foreground">
                  <IconFileDescription className="h-4 w-4" />
                  Descrição do Serviço
                </div>
                <div
                  className="prose prose-sm max-w-none bg-muted/30 rounded-lg p-4"
                  role="article"
                  aria-label="Descrição do serviço"
                >
                  <p className="text-foreground leading-relaxed whitespace-pre-wrap">
                    {os.descricao || "Nenhuma descrição fornecida."}
                  </p>
                </div>
              </div>
            </CardContent>
          </Card>

          {/* 📋 Seção de Alocações - AGORA COM DADOS REAIS */}
          <Card>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <IconUser className="h-5 w-5" />
                Trabalhadores Alocados
                {totalTrabalhadores > 0 && (
                  <Badge variant="secondary" className="ml-2">
                    {totalTrabalhadores}
                  </Badge>
                )}
              </CardTitle>
            </CardHeader>
            <CardContent>
              {os.alocacoes && os.alocacoes.length > 0 ? (
                <div className="space-y-4">
                  {os.alocacoes.map((alocacao, index) => (
                    <div
                      key={alocacao.id}
                      className="border rounded-lg p-4 space-y-3"
                    >
                      <div className="flex items-center justify-between">
                        <div className="flex items-center gap-3">
                          <div className="w-8 h-8 bg-primary/10 rounded-full flex items-center justify-center">
                            <IconUser className="h-4 w-4 text-primary" />
                          </div>
                          <div>
                            <p className="font-medium">
                              Trabalhador {index + 1}
                            </p>
                            <code className="text-xs text-muted-foreground bg-muted px-1.5 py-0.5 rounded">
                              {alocacao.trabalhadorIdentificacao}
                            </code>
                          </div>
                        </div>
                        <Badge variant="outline" className="gap-1">
                          <IconCash className="h-3 w-3" />
                          {formatCentsToReais(alocacao.valorTotal)}
                        </Badge>
                      </div>

                      {/* Detalhes do Serviço */}
                      {alocacao.detalhes && alocacao.detalhes.length > 0 && (
                        <div className="space-y-2">
                          <div className="flex items-center gap-2 text-sm font-medium text-muted-foreground">
                            <IconListDetails className="h-4 w-4" />
                            Serviços Realizados
                          </div>
                          <div className="space-y-2">
                            {alocacao.detalhes.map((detalhe) => (
                              <div
                                key={detalhe.id}
                                className="flex justify-between items-center py-2 px-3 bg-muted/30 rounded"
                              >
                                <span className="text-sm">
                                  {detalhe.descricao}
                                </span>
                                <Badge variant="secondary" className="text-xs">
                                  {formatCentsToReais(detalhe.valor)}
                                </Badge>
                              </div>
                            ))}
                          </div>
                        </div>
                      )}
                    </div>
                  ))}
                </div>
              ) : (
                <div className="text-center py-8 text-muted-foreground">
                  <IconUser className="h-12 w-12 mx-auto mb-4 opacity-50" />
                  <p className="font-medium">Nenhum trabalhador alocado</p>
                  <p className="text-sm mt-1">
                    Adicione trabalhadores para esta ordem de serviço
                  </p>
                  <Button className="mt-4 gap-2">
                    <IconUserPlus className="h-4 w-4" />
                    Alocar Primeiro Trabalhador
                  </Button>
                </div>
              )}
            </CardContent>
          </Card>
        </div>

        {/* 🎯 COLUNA LATERAL (AÇÕES E RESUMO) */}
        <div className="space-y-6">
          {/* Card de Ações Rápidas */}
          <Card className="shadow-sm">
            <CardHeader>
              <CardTitle className="text-lg">Ações Rápidas</CardTitle>
            </CardHeader>
            <CardContent className="space-y-3">
              <Button className="w-full justify-start gap-2" variant="outline">
                <IconEdit className="h-4 w-4" />
                Editar Informações
              </Button>
              <Button className="w-full justify-start gap-2" variant="outline">
                <IconRefresh className="h-4 w-4" />
                Alterar Status
              </Button>
              <Button className="w-full justify-start gap-2">
                <IconUserPlus className="h-4 w-4" />
                Gerenciar Alocações
              </Button>
            </CardContent>
          </Card>

          {/* Card de Resumo - AGORA COM DADOS REAIS */}
          <Card className="shadow-sm">
            <CardHeader>
              <CardTitle className="text-lg">Resumo</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <div className="grid grid-cols-2 gap-4 text-sm">
                <div className="space-y-1">
                  <p className="text-muted-foreground">Criada em</p>
                  <p className="font-medium">
                    {format(dataCriacao, "dd/MM/yy", { locale: ptBR })}
                  </p>
                </div>
                <div className="space-y-1">
                  <p className="text-muted-foreground">Trabalhadores</p>
                  <p className="font-medium">{totalTrabalhadores}</p>
                </div>
                <div className="space-y-1">
                  <p className="text-muted-foreground">Valor Total</p>
                  <p className="font-medium text-green-600">
                    {formatCentsToReais(valorTotalOS)}
                  </p>
                </div>
                <div className="space-y-1">
                  <p className="text-muted-foreground">Prioridade</p>
                  <Badge variant="outline" className="text-xs">
                    Normal
                  </Badge>
                </div>
              </div>

              <Separator />

              <div className="space-y-2">
                <p className="text-sm text-muted-foreground">
                  Última Atualização
                </p>
                <p className="text-sm font-medium">
                  {formatDistanceToNow(ultimaAtualizacao, {
                    locale: ptBR,
                    addSuffix: true,
                  })}
                </p>
              </div>
            </CardContent>
          </Card>

          {/* Card de Metadados */}
          <Card className="bg-muted/50">
            <CardContent className="pt-6">
              <div className="space-y-3 text-sm">
                <div>
                  <p className="font-medium text-muted-foreground">
                    ID Completo
                  </p>
                  <code
                    className="text-xs break-all bg-background p-2 rounded border block mt-1"
                    aria-label="ID completo da ordem de serviço"
                  >
                    {os.id}
                  </code>
                </div>
                <div>
                  <p className="font-medium text-muted-foreground">
                    ID Cliente
                  </p>
                  <code className="text-xs break-all bg-background p-2 rounded border block mt-1">
                    {os.empresaClienteId}
                  </code>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </div>
  );
}
