interface ErrorListProps {
  errors: string[];
}

/**
 * Um componente simples para renderizar uma lista de erros de forma amigável.
 * Se houver 1 erro, renderiza um <p>.
 * Se houver múltiplos erros, renderiza uma <ul>.
 */
export function ErrorList({ errors }: ErrorListProps) {
  // Se não houver erros, não renderiza nada
  if (!errors || errors.length === 0) {
    return null;
  }

  // Se houver apenas um erro, mostra como um parágrafo simples
  if (errors.length === 1) {
    return <p>{errors[0]}</p>;
  }

  // Se houver múltiplos erros, cria uma lista de itens
  return (
    <ul className="list-disc space-y-1 pl-5 text-left">
      {errors.map((error, index) => (
        <li key={index}>{error}</li>
      ))}
    </ul>
  );
}
