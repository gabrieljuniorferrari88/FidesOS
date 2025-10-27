import FormCardSkeleton from "@/components/form-card-skeleton";
import PageContainer from "@/components/layout/page-container";
import ProductViewPage from "@/features/products/components/product-view-page";
import { Suspense } from "react";

export const metadata = {
  title: "Dashboard : Ordens de Serviço View",
};

type PageProps = { params: Promise<{ ordemServicoId: string }> };

export default async function Page(props: PageProps) {
  const params = await props.params;
  return (
    <PageContainer scrollable>
      <div className="flex-1 space-y-4">
        <Suspense fallback={<FormCardSkeleton />}>
          <ProductViewPage productId={params.ordemServicoId} />
        </Suspense>
      </div>
    </PageContainer>
  );
}
