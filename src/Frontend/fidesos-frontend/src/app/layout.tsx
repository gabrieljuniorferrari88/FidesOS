import { AuthInitializer } from "@/components/AuthInitializer";
import Providers from "@/components/Providers";
import { Toaster } from "@/components/ui/sonner";
import "@/styles/globals.css";
import type { Metadata } from "next";
import { ThemeProvider } from "next-themes";
import { Roboto } from "next/font/google";
import { cookies } from "next/headers";

const roboto = Roboto({
	variable: "--font-sans",
	subsets: ["latin"],
});

export const metadata: Metadata = {
	title: "FidesOS",
	description: "Sistema de Gestão de Ordens de Serviço",
};

export default async function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	const cookieStore = await cookies();
	const themeCookie = cookieStore.get("theme")?.value;

	return (
		<html
			lang="pt-BR"
			suppressHydrationWarning={true}
			// Remove qualquer classe manual do HTML
		>
			<body className={`${roboto.className} min-h-screen antialiased`}>
				<Providers>
					<ThemeProvider
						attribute="class"
						defaultTheme={themeCookie || "light"}
						enableSystem={false}
						disableTransitionOnChange
						// Adicione storageKey para sincronizar com localStorage
						storageKey="fidesos-theme"
					>
						<AuthInitializer />
						{children}
						<Toaster expand={true} closeButton richColors />
					</ThemeProvider>
				</Providers>
			</body>
		</html>
	);
}
