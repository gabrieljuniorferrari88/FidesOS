"use client";

import { Palette } from "lucide-react";
import { useTheme } from "next-themes";

import { Button } from "@/components/ui/button";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";

export function ThemeSwitcher() {
	const { setTheme, theme } = useTheme();

	// Remove a Server Action e usa apenas o client state
	const handleThemeChange = (newTheme: string) => {
		setTheme(newTheme);
		// O next-themes já salva automaticamente no localStorage
		// E o cookie será sincronizado via layout
	};

	return (
		<DropdownMenu>
			<DropdownMenuTrigger asChild>
				<Button size="icon" variant="ghost">
					<Palette className="h-[1.2rem] w-[1.2rem]" />
					<span className="sr-only">Toggle theme</span>
				</Button>
			</DropdownMenuTrigger>
			<DropdownMenuContent align="end">
				<DropdownMenuItem onClick={() => handleThemeChange("light")}>
					Claro
				</DropdownMenuItem>
				<DropdownMenuItem onClick={() => handleThemeChange("dark")}>
					Escuro
				</DropdownMenuItem>
				<DropdownMenuItem onClick={() => handleThemeChange("supabase")}>
					Supabase
				</DropdownMenuItem>
				<DropdownMenuItem
					onClick={() => handleThemeChange("supabase-dark")}
				>
					Supabase Escuro
				</DropdownMenuItem>
				<DropdownMenuItem
					onClick={() => handleThemeChange("catpuccini")}
				>
					Catpuccini
				</DropdownMenuItem>
				<DropdownMenuItem
					onClick={() => handleThemeChange("catpuccini-dark")}
				>
					Catpuccini Escuro
				</DropdownMenuItem>
			</DropdownMenuContent>
		</DropdownMenu>
	);
}
