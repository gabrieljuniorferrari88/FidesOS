import { searchParamsCache } from "@/lib/searchparams";
import { cookies } from "next/headers";
import { listarOrdensDeServico } from "../services/os-service";
import { OrdemDeServicoTable } from "./os-tables";
import { columns } from "./os-tables/columns";

type OrdemDeServicoListingPage = {};

export default async function OrdemDeServicoListingPage({}: OrdemDeServicoListingPage) {
  const page = searchParamsCache.get("page");
  const search = searchParamsCache.get("name");
  const pageLimit = searchParamsCache.get("perPage");
  const categories = searchParamsCache.get("category");

  const cookieStore = await cookies();
  const token = cookieStore.get("accessToken")?.value;

  const filters = {
    page: Number(page) || 1,
    perPage: Number(pageLimit) || 10,
    // ...(search && { search }),
    // ...(categories && { categories: categories })
  };

  const data = await listarOrdensDeServico(filters, token);

  const totalOs = data.totalDeItens;

  return (
    <OrdemDeServicoTable
      data={data.itens}
      totalItems={totalOs}
      columns={columns}
    />
  );
}
