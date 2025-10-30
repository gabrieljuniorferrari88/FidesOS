"use client";

import { usePathname } from "next/navigation";
import { useMemo } from "react";

type BreadcrumbItem = {
  title: string;
  link: string;
};

// This allows to add custom title as well
const routeMapping: Record<string, BreadcrumbItem[]> = {
  "/dashboard": [{ title: "Dashboard", link: "/dashboard" }],
  "/dashboard/employee": [
    { title: "Dashboard", link: "/dashboard" },
    { title: "Employee", link: "/dashboard/employee" },
  ],
  "/dashboard/product": [
    { title: "Dashboard", link: "/dashboard" },
    { title: "Product", link: "/dashboard/product" },
  ],
  // Add more custom mappings as needed
  "/dashboard/ordem-de-servico": [
    { title: "Dashboard", link: "/dashboard" },
    { title: "Ordem de Serviço", link: "/dashboard/ordem-de-servico" },
  ],
};

export function useBreadcrumbs() {
  const pathname = usePathname();

  const breadcrumbs = useMemo(() => {
    const cleanPathname = pathname
      .replace(/\/[0-9a-fA-F-]{36}/g, "")
      .replace(/\/\d+/g, "");

    if (routeMapping[cleanPathname]) {
      return routeMapping[cleanPathname];
    }

    // If no exact match, fall back to generating breadcrumbs from the path
    const segments = pathname.split("/").filter(Boolean);
    const breadcrumbs: BreadcrumbItem[] = [];
    let currentPath = "";

    segments.map((segment, index) => {
      const isId = /^[0-9a-fA-F-]{36}$/.test(segment) || /^\d+$/.test(segment);
      if (isId) return;

      currentPath += `/${segment}`;

      breadcrumbs.push({
        title:
          segment.charAt(0).toUpperCase() + segment.slice(1).replace(/-/g, " "),
        link: currentPath,
      });
    });

    return breadcrumbs;
  }, [pathname]);

  return breadcrumbs;
}
