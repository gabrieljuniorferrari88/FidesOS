"use client";

import NextError from "next/error";
import { useEffect } from "react";

export default function GlobalError({
  error,
}: {
  error: Error & { digest?: string };
}) {
  useEffect(() => {
    console.error(error);
  }, [error]);

  return (
    <html>
      <body>
        <NextError statusCode={500} title="Erro Inesperado" />
      </body>
    </html>
  );
}
