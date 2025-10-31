export function formatDate(
  date: Date | string | number | undefined,
  opts: Intl.DateTimeFormatOptions = {}
) {
  if (!date) return '';

  try {
    return new Intl.DateTimeFormat('pt-BR', {
      month: opts.month ?? 'long',
      day: opts.day ?? 'numeric',
      year: opts.year ?? 'numeric',
      ...opts
    }).format(new Date(date));
  } catch (_err) {
    return '';
  }
}


/**
 * Utilitários para formatação de valores monetários
 * Considerando que valores são salvos em centavos no banco (35000 = R$ 350,00)
 */

/**
 * Converte valor em centavos para reais (divide por 100)
 */
export function centsToReais(cents: number): number {
  return cents / 100;
}

/**
 * Formata valor em centavos para string em reais (R$ 350,00)
 */
export function formatCentsToReais(cents: number): string {
  const reais = centsToReais(cents);
  return reais.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  });
}

/**
 * Formata valor em reais para string (R$ 350,00)
 */
export function formatReais(value: number): string {
  return value.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  });
}

/**
 * Converte reais para centavos (multiplica por 100)
 * Útil para forms que vão salvar no banco
 */
export function reaisToCents(reais: number): number {
  return Math.round(reais * 100);
}

/**
 * Calcula o total de uma lista de itens em centavos
 */
export function calculateTotalCents(items: { valor: number }[]): number {
  return items.reduce((total, item) => total + item.valor, 0);
}

/**
 * Formata o total de uma lista de itens diretamente para reais
 */
export function formatItemsTotalReais(items: { valor: number }[]): string {
  const totalCents = calculateTotalCents(items);
  return formatCentsToReais(totalCents);
}