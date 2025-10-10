import { jwtVerify } from 'jose';
import { type NextRequest, NextResponse } from 'next/server';

const JWT_SECRET = new TextEncoder().encode(process.env.JWT_SIGNING_KEY!);

export async function middleware(req: NextRequest) {
    const token = req.cookies.get("accessToken")?.value;
    const { pathname } = req.nextUrl;

    // Se NÃO existe token e a rota é protegida, redireciona para o login
    if (!token && pathname.startsWith('/dashboard')) {
        return NextResponse.redirect(new URL('/login', req.url));
    }

    // Se EXISTE um token...
    if (token) {
        try {
            // ...tentamos validar. Se falhar, vai para o catch.
            await jwtVerify(token, JWT_SECRET);

            // Se o token é VÁLIDO e o usuário tenta ir para o login, redireciona para o dashboard.
            if (pathname.startsWith('/login')) {
                return NextResponse.redirect(new URL('/dashboard', req.url));
            }
        } catch (error) {
            // O TOKEN É INVÁLIDO (assinatura errada, expirado, etc.)!
            // Redireciona para o login e limpa o cookie ruim.
            const response = NextResponse.redirect(new URL('/login', req.url));
            response.cookies.delete('accessToken');
            return response;
        }
    }

    // Em todos os outros casos (ex: rota pública sem token), permite o acesso.
    return NextResponse.next();
}

export const config = {
    matcher: [
        '/dashboard/:path*',
        '/login',
    ],
};
