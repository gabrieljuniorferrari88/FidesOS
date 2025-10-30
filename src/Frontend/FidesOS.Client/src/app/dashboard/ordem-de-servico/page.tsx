import PageContainer from "@/components/layout/page-container";
import { buttonVariants } from "@/components/ui/button";
import { Heading } from "@/components/ui/heading";
import { Separator } from "@/components/ui/separator";
import { DataTableSkeleton } from "@/components/ui/table/data-table-skeleton";
import OrdemDeServicoListingPage from "@/features/ordem-de-servico/components/os-listing";
import { searchParamsCache } from "@/lib/searchparams";
import { cn } from "@/lib/utils";
import { IconPlus } from "@tabler/icons-react";
import Link from "next/link";
import { SearchParams } from "nuqs/server";
import { Suspense } from "react";

type pageProps = {
  searchParams: Promise<SearchParams>;
};

export default async function OrdemDeServicoPage(props: pageProps) {
  const searchParams = await props.searchParams;

  searchParamsCache.parse(searchParams);

  return (
    <PageContainer scrollable={false}>
      <div className="flex flex-1 flex-col space-y-4">
        <div className="flex items-start justify-between">
          <Heading
            title="Ordens de Serviços"
            description="Gerencie suas ordens de serviço"
          />
          <Link
            href="/dashboard/ordem-de-servico/novo"
            className={cn(buttonVariants(), "text-xs md:text-sm")}
          >
            <IconPlus className="mr-2 h-4 w-4" /> Add Novo
          </Link>
        </div>
        <Separator />
        <Suspense
          // key={key}
          fallback={
            <DataTableSkeleton columnCount={3} rowCount={8} filterCount={2} />
          }
        >
          <OrdemDeServicoListingPage />
        </Suspense>
      </div>
    </PageContainer>
  );
}
