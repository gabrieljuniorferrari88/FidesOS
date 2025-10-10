import { Button } from "@/components/ui/button";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { CircleUser } from "lucide-react";
// No futuro, podemos adicionar um Sheet para o menu mobile
// import { Sheet, SheetContent, SheetTrigger } from '@/components/ui/sheet';

export default function Header() {
	return (
		<header className="flex h-14 items-center gap-4 border-b bg-muted/40 px-4 lg:h-[60px] lg:px-6">
			{/* Aqui virá a lógica do menu mobile no futuro */}
			<div className="w-full flex-1">
				{/* Podemos adicionar um campo de busca aqui no futuro */}
			</div>
			<DropdownMenu>
				<DropdownMenuTrigger asChild>
					<Button
						variant="secondary"
						size="icon"
						className="rounded-full"
					>
						<CircleUser className="h-5 w-5" />
						<span className="sr-only">Toggle user menu</span>
					</Button>
				</DropdownMenuTrigger>
				<DropdownMenuContent align="end">
					<DropdownMenuLabel>Minha Conta</DropdownMenuLabel>
					<DropdownMenuSeparator />
					<DropdownMenuItem>Perfil</DropdownMenuItem>
					<DropdownMenuItem>Configurações</DropdownMenuItem>
					<DropdownMenuSeparator />
					<DropdownMenuItem>Logout</DropdownMenuItem>
				</DropdownMenuContent>
			</DropdownMenu>
		</header>
	);
}
