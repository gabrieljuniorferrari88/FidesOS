import { z } from 'zod';

// Schema para o formulário de ATUALIZAR PERFIL
export const updateProfileSchema = z.object({
  nome: z.string().min(3, { message: "O nome deve ter pelo menos 3 caracteres." }),
  email: z.string().email({ message: "Por favor, insira um e-mail válido." }),
});

// Exporta o tipo inferido para usarmos no nosso formulário
export type UpdateProfileFormValues = z.infer<typeof updateProfileSchema>;