import { RespostaOrdemDeServicoDetalhadaJson } from "@/types/api-resposta";
import { cookies } from "next/headers";
import { notFound } from "next/navigation";
import { buscarOrdemServicoPorId } from "../services/os-service";
import OrdemDeServicoForm from "./os-form";

type TOsViewPageProps = {
  osId: string;
};

export default async function OsViewPage({ osId }: TOsViewPageProps) {
  let os: RespostaOrdemDeServicoDetalhadaJson | null = null;
  let pageTitle = "Criar nova OS";

  const cookieStore = await cookies();
  const token = cookieStore.get("accessToken")?.value;

  if (osId !== "new") {
    const data = await buscarOrdemServicoPorId(osId, token);
    if (!data) {
      notFound();
    }
    os = data;
    pageTitle = `Editar OS`;
  }

  return <OrdemDeServicoForm initialData={os} pageTitle={pageTitle} />;
}
