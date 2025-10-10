"use client";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { getInitials } from "@/lib/utils";
import { useAuthStore } from "@/store/authStore";
import { LogOut } from "lucide-react";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import defaultUser from "../../../../../public/images/default-user.png";

export function AccountSwitcher() {
	const { user, logout, isLoading, loadUser, isAuthenticated } =
		useAuthStore();
	const router = useRouter();
	const [isMounted, setIsMounted] = useState(false);

	// Carrega o usuário quando o componente monta
	useEffect(() => {
		setIsMounted(true);
		// Se está autenticado mas não tem user, carrega os dados
		if (isAuthenticated && !user) {
			loadUser();
		}
	}, [isAuthenticated, user, loadUser]);

	const handleLogout = () => {
		logout();
		router.push("/login");
	};

	// Loading state
	if (!isMounted || isLoading) {
		return (
			<Avatar className="h-9 w-9 cursor-pointer">
				<AvatarFallback className="rounded-lg animate-pulse bg-muted">
					...
				</AvatarFallback>
			</Avatar>
		);
	}

	// Se não está autenticado ou não tem usuário
	if (!isAuthenticated || !user) {
		return (
			<DropdownMenu>
				<DropdownMenuTrigger asChild>
					<Avatar className="h-9 w-9 cursor-pointer">
						<AvatarFallback className="rounded-lg">
							?
						</AvatarFallback>
					</Avatar>
				</DropdownMenuTrigger>
				<DropdownMenuContent align="end" className="w-56">
					<DropdownMenuLabel className="font-normal">
						<div className="flex flex-col space-y-1">
							<p className="text-sm font-medium leading-none">
								Não autenticado
							</p>
							<p className="text-xs leading-none text-muted-foreground">
								Faça login para continuar
							</p>
						</div>
					</DropdownMenuLabel>
					<DropdownMenuSeparator />
					<DropdownMenuItem
						onClick={() => router.push("/login")}
						className="cursor-pointer"
					>
						<LogOut className="mr-2 h-4 w-4" />
						<span>Fazer Login</span>
					</DropdownMenuItem>
				</DropdownMenuContent>
			</DropdownMenu>
		);
	}

	// Renderiza normalmente quando tem usuário autenticado
	return (
		<DropdownMenu>
			<DropdownMenuTrigger asChild>
				<Avatar className="h-9 w-9 cursor-pointer">
					<AvatarImage
						// src={user.avatar || undefined}
						src={defaultUser.src}
						alt={user.nome}
					/>
					<AvatarFallback className="rounded-lg">
						{getInitials(user.nome)}
					</AvatarFallback>
				</Avatar>
			</DropdownMenuTrigger>
			<DropdownMenuContent align="end" className="w-56">
				<DropdownMenuLabel className="font-normal">
					<div className="flex flex-col space-y-1">
						<p className="text-sm font-medium leading-none">
							{user.nome}
						</p>
						<p className="text-xs leading-none text-muted-foreground">
							{user.email}
						</p>
					</div>
				</DropdownMenuLabel>
				<DropdownMenuSeparator />
				<DropdownMenuItem
					onClick={handleLogout}
					className="cursor-pointer text-red-500 focus:bg-red-50 focus:text-red-600"
				>
					<LogOut className="mr-2 h-4 w-4" />
					<span>Sair</span>
				</DropdownMenuItem>
			</DropdownMenuContent>
		</DropdownMenu>
	);
}
