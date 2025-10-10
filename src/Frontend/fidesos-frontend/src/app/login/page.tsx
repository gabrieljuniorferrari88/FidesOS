"use client";

import { useAuthStore } from "@/store/authStore";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { LoginFormInputs, loginSchema } from "@/api/schemas/loginSchema";
import { authService } from "@/api/services/authService";
import { Button } from "@/components/ui/button";
import {
	Card,
	CardContent,
	CardDescription,
	CardHeader,
	CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export default function LoginPage() {
	const { login: loginInStore, isAuthenticated } = useAuthStore();
	const router = useRouter();

	// Este estado é a chave para resolver a hidratação
	const [hasMounted, setHasMounted] = useState(false);

	// Este useEffect garante que o 'hasMounted' só se torna 'true' APÓS a primeira renderização no cliente
	useEffect(() => {
		setHasMounted(true);
	}, []);

	// Este useEffect agora só tenta redirecionar se a página já montou E o usuário está logado
	useEffect(() => {
		if (hasMounted && isAuthenticated) {
			router.push("/dashboard");
		}
	}, [hasMounted, isAuthenticated, router]);

	// Configuração do formulário e da mutação (só devem ser chamados se o formulário for visível)
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm<LoginFormInputs>({
		resolver: zodResolver(loginSchema),
	});

	const { mutate, isPending } = useMutation({
		mutationFn: authService,
		onSuccess: (data) => {
			toast.success("Login bem-sucedido!", {
				description: `Bem-vindo de volta, ${data.nome}!`,
			});
			loginInStore(data.tokens.accessToken);
			router.push("/dashboard");
		},
		onError: (error) => {
			toast.error("Falha no login", {
				description:
					"E-mail ou senha inválidos. Por favor, tente novamente.",
			});
		},
	});

	const onSubmit = (data: LoginFormInputs) => {
		mutate(data);
	};

	// CONDIÇÃO DE RENDERIZAÇÃO CRÍTICA
	// Se a página ainda não montou no cliente, ou se já sabemos que o usuário está logado
	// (e será redirecionado), nós não renderizamos o formulário.
	// Isso garante que os hooks (useForm, useMutation) não são chamados condicionalmente.
	if (!hasMounted || (hasMounted && isAuthenticated)) {
		return <p>Carregando...</p>; // Ou um componente de Spinner/Loader
	}

	return (
		<main className="min-h-screen flex items-center justify-center bg-muted/40">
			<Card className="max-w-md w-full">
				<CardHeader className="text-center">
					<CardTitle className="text-2xl font-bold">
						FidesOS - Acesso
					</CardTitle>
					<CardDescription>
						Entre com suas credenciais para acessar o sistema.
					</CardDescription>
				</CardHeader>
				<CardContent>
					<form
						onSubmit={handleSubmit(onSubmit)}
						className="space-y-4"
					>
						<div className="space-y-2">
							<Label htmlFor="email">E-mail</Label>
							<Input
								id="email"
								type="email"
								placeholder="seu@email.com"
								{...register("email")}
							/>
							{errors.email && (
								<p className="text-sm text-red-500">
									{errors.email.message}
								</p>
							)}
						</div>
						<div className="space-y-2">
							<Label htmlFor="password">Senha</Label>
							<Input
								id="password"
								type="password"
								{...register("senha")}
							/>
							{errors.senha && (
								<p className="text-sm text-red-500">
									{errors.senha.message}
								</p>
							)}
						</div>
						<Button
							className="w-full"
							type="submit"
							disabled={isPending}
						>
							{isPending ? "Carregando..." : "Entrar"}
						</Button>
					</form>
				</CardContent>
			</Card>
		</main>
	);
}
