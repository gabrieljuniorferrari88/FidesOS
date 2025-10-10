import { z } from 'zod';

// Criamos um "schema" que descreve o formato e as regras dos dados do nosso formulário
export const loginSchema = z.object({
  email: z.string().email({ message: "Por favor, insira um e-mail válido." }),
  senha: z.string().min(1, { message: "A senha é obrigatória." }),
});

// O 'infer' do Zod é uma ferramenta poderosa. Ele cria um tipo TypeScript
// automaticamente a partir do nosso schema. Isso evita que a gente precise
// escrever a mesma coisa duas vezes.
export type LoginFormInputs = z.infer<typeof loginSchema>;
