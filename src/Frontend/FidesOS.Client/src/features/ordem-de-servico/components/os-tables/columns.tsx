"use client";
import { Badge } from "@/components/ui/badge";
import { DataTableColumnHeader } from "@/components/ui/table/data-table-column-header";
import { RespostaOrdemDeServicoResumidaJson } from "@/types/api-resposta";
import { Column, ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { ptBR } from "date-fns/locale";
import { Text } from "lucide-react";
import { CellAction } from "./cell-action";
import { STATUS_OPTIONS } from "./options";

const getStatusInfo = (status: number) => {
  switch (status) {
    case 0:
      return { text: "Pendente", variant: "default" as const };
    case 1:
      return { text: "Agendada", variant: "secondary" as const };
    case 2:
      return { text: "Em Andamento", variant: "outline" as const };
    case 3:
      return { text: "Concluída", variant: "destructive" as const };
    case 4:
      return { text: "Cancelada", variant: "destructive" as const };
    default:
      return { text: "Desconhecido", variant: "default" as const };
  }
};

export const columns: ColumnDef<RespostaOrdemDeServicoResumidaJson>[] = [
  {
    id: "descricao",
    accessorKey: "descricao",
    header: ({
      column,
    }: {
      column: Column<RespostaOrdemDeServicoResumidaJson, unknown>;
    }) => <DataTableColumnHeader column={column} title="Descrição" />,
    cell: ({ cell }) => (
      <div>
        {cell.getValue<RespostaOrdemDeServicoResumidaJson["descricao"]>()}
      </div>
    ),
    meta: {
      label: "Descrição",
      placeholder: "Buscar ordem de serviço products...",
      variant: "text",
      icon: Text,
    },
    enableColumnFilter: true,
  },
  {
    id: "status",
    accessorKey: "status",
    header: ({
      column,
    }: {
      column: Column<RespostaOrdemDeServicoResumidaJson, unknown>;
    }) => <DataTableColumnHeader column={column} title="Status" />,
    cell: ({ row }) => {
      const statusInfo = getStatusInfo(row.original.status);
      return (
        <div className="w-[100px]">
          <Badge variant={statusInfo.variant}>{statusInfo.text}</Badge>
        </div>
      );
    },
    enableColumnFilter: true,
    meta: {
      label: "Status",
      variant: "multiSelect",
      options: STATUS_OPTIONS,
    },
  },
  {
    id: "dataAgendamento",
    accessorKey: "dataAgendamento",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Agendamento" />
    ),
    cell: ({ row }) => {
      const data = new Date(row.original.dataAgendamento);
      return format(data, "dd/MM/yyyy HH:mm", { locale: ptBR });
    },
    meta: {
      label: "Data Agendamento",
      variant: "date",
    },
  },
  {
    id: "actions",
    cell: ({ row }) => <CellAction row={row} />,
  },
];
