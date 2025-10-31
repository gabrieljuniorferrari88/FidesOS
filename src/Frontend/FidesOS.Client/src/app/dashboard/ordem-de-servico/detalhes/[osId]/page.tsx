import { notFound } from "next/navigation";

import PageContainer from "@/components/layout/page-container";
import OsDetalhesView from "@/features/ordem-de-servico/components/os-detalhes-view";
import { buscarOrdemServicoPorId } from "@/features/ordem-de-servico/services/os-service";
import { cookies } from "next/headers";

interface OsDetalhePageProps {
  params: {
    osId: string; // O nome 'osId' deve ser o mesmo da pasta [osId]
  };
}

export const metadata = {
  title: "Dashboard : Detalhes da OS",
};

export default async function OrdemDeServicoDetalhePage({
  params,
}: OsDetalhePageProps) {
  const { osId } = params;

  const cookieStore = await cookies();
  const token = cookieStore.get("accessToken")?.value;

  try {
    const os = await buscarOrdemServicoPorId(osId, token);

    if (!os) {
      notFound();
    }

    return (
      <PageContainer>
        <OsDetalhesView initialData={os} />
      </PageContainer>
    );
  } catch (error) {
    console.error("[Detalhe OS] Erro ao buscar OS:", error);
    notFound();
  }
}
