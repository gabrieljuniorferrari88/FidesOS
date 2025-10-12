import { jwtVerify } from 'jose';
import { type NextRequest, NextResponse } from 'next/server';

// A chave secreta para validar a assinatura do token.
// Garanta que esta variável de ambiente NÃO tenha o prefixo NEXT_PUBLIC_
const JWT_SECRET = new TextEncoder().encode(process.env.JWT_SIGNING_KEY!);

export async function middleware(req: NextRequest) {
    // 1. Pega o token do cookie e o caminho da URL.
    const token = req.cookies.get("accessToken")?.value;
    const { pathname } = req.nextUrl;

    // 2. Cenário: Usuário NÃO TEM token e tenta acessar uma rota protegida.
    // As rotas de autenticação são públicas, então as excluímos da verificação.
    if (!token && !pathname.startsWith('/auth')) {
        // Redireciona para a tela de login.
        return NextResponse.redirect(new URL('/auth/sign-in', req.url));
    }

    // 3. Cenário: Usuário TEM um token.
    if (token) {
        try {
            // 3.1 Tenta validar o token. Se a assinatura ou o tempo expirarem, ele vai para o 'catch'.
            await jwtVerify(token, JWT_SECRET);

            // 3.2 Se a validação passou, o token é bom.
            // Agora, se o usuário logado tenta acessar as páginas de login/registro...
            if (pathname.startsWith('/auth')) {
                // ...nós o redirecionamos para o dashboard.
                return NextResponse.redirect(new URL('/dashboard/overview', req.url));
            }
        } catch (error) {
            // 3.3 O TOKEN É INVÁLIDO! (Assinatura errada, expirado, etc.)
            // Criamos uma resposta para redirecionar para o login...
            const response = NextResponse.redirect(new URL('/auth/sign-in', req.url));
            // ...e nessa resposta, mandamos o comando para o navegador DELETAR o cookie inválido.
            response.cookies.delete('accessToken');
            return response;
        }
    }

    // 4. Se nenhum dos casos acima se aplicar, permite que a requisição continue.
    return NextResponse.next();
}

// O 'matcher' define em quais rotas este middleware vai rodar.
// É mais eficiente do que rodar em todas as requisições.
export const config = {
    matcher: [
        // Roda em todas as rotas, exceto as de arquivos estáticos e da API interna do Next.js
        '/((?!api|_next/static|_next/image|favicon.ico).*)',
    ],
};