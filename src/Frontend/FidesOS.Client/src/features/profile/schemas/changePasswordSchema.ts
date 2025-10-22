import { z } from 'zod';

// Schema para o formulário de ALTERAR SENHA
export const changePasswordSchema = z.object({
  senha: z.string().min(1, { message: "A senha atual é obrigatória." }),
  novaSenha: z.string().min(8, { message: "A nova senha deve ter pelo menos 8 caracteres." }),
});

export type ChangePasswordFormValues = z.infer<typeof changePasswordSchema>;