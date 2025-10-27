import { fakeProducts, Product } from "@/constants/mock-api";
import { notFound } from "next/navigation";
import ProductForm from "./os-form";

type TOsViewPageProps = {
  ordemServicoId: string;
};

export default async function OsViewPage({ ordemServicoId }: TOsViewPageProps) {
  let os = null;
  let pageTitle = "Create New OS";

  if (ordemServicoId !== "new") {
    const data = await fakeProducts.getProductById(Number(ordemServicoId));
    os = data.product as Product;
    if (!os) {
      notFound();
    }
    pageTitle = `Edit OS`;
  }

  return <ProductForm initialData={os} pageTitle={pageTitle} />;
}
