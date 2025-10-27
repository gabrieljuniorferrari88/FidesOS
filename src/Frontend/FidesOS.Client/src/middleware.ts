import { jwtVerify } from 'jose';
import { type NextRequest, NextResponse } from 'next/server';

const JWT_SECRET = new TextEncoder().encode(process.env.JWT_SIGNING_KEY!);

export async function middleware(req: NextRequest) {
    const token = req.cookies.get("accessToken")?.value;
    const { pathname } = req.nextUrl;

    if (!token && !pathname.startsWith('/auth')) {
        return NextResponse.redirect(new URL('/auth/sign-in', req.url));
    }

    if (token) {
        try {
            await jwtVerify(token, JWT_SECRET);

            if (pathname.startsWith('/auth')) {
                return NextResponse.redirect(new URL('/dashboard/overview', req.url));
            }
        } catch (error) {
            const response = NextResponse.redirect(new URL('/auth/sign-in', req.url));
            response.cookies.delete('accessToken');
            return response;
        }
    }

    return NextResponse.next();
}

export const config = {
    matcher: [
        // Roda em todas as rotas, exceto as de arquivos estáticos e da API interna do Next.js
        '/((?!api|_next/static|_next/image|favicon.ico).*)',
    ],
};