"use client";

import {
	CommandDialog,
	CommandEmpty,
	CommandGroup,
	CommandInput,
	CommandItem,
	CommandList,
	CommandSeparator,
} from "@/components/ui/command";
import {
	BookA,
	ChartLine,
	ChartPie,
	Forklift,
	Grid2X2,
	Search,
	ShoppingBag,
} from "lucide-react";
import { useRouter } from "next/navigation";
import * as React from "react";

const searchItems = [
	{
		group: "Dashboards",
		icon: ChartPie,
		label: "Default",
		link: "/dashboard/default",
	},
	{
		group: "Dashboards",
		icon: ChartPie,
		label: "About",
		link: "/dashboard/about",
	},
	{ group: "Dashboards", icon: Grid2X2, label: "CRM", disabled: true },
	{
		group: "Dashboards",
		icon: ChartLine,
		label: "Analytics",
		disabled: true,
	},
	{
		group: "Dashboards",
		icon: ShoppingBag,
		label: "E-Commerce",
		disabled: true,
	},
	{ group: "Dashboards", icon: BookA, label: "Academy", disabled: true },
	{ group: "Dashboards", icon: Forklift, label: "Logistics", disabled: true },
	{ group: "Authentication", label: "Login v1" },
	{ group: "Authentication", label: "Register v1" },
];

export function SearchDialog() {
	const [open, setOpen] = React.useState(false);
	const router = useRouter(); // Inicialize o useRouter
	React.useEffect(() => {
		const down = (e: KeyboardEvent) => {
			if (e.key === "j" && (e.metaKey || e.ctrlKey)) {
				e.preventDefault();
				setOpen((open) => !open);
			}
		};
		document.addEventListener("keydown", down);
		return () => document.removeEventListener("keydown", down);
	}, []);

	return (
		<>
			<div
				className="text-muted-foreground flex cursor-pointer items-center gap-2 text-sm"
				onClick={() => setOpen(true)}
			>
				<Search className="size-4" />
				Search
				<kbd className="bg-muted inline-flex h-5 items-center gap-1 rounded border px-1.5 text-[10px] font-medium select-none">
					<span className="text-xs">⌘</span>J
				</kbd>
			</div>
			<CommandDialog open={open} onOpenChange={setOpen}>
				<CommandInput placeholder="Search dashboards, users, and more…" />
				<CommandList>
					<CommandEmpty>No results found.</CommandEmpty>
					{[...new Set(searchItems.map((item) => item.group))].map(
						(group, i) => (
							<React.Fragment key={group}>
								{i !== 0 && <CommandSeparator />}
								<CommandGroup heading={group} key={group}>
									{searchItems
										.filter((item) => item.group === group)
										.map((item) => (
											<CommandItem
												className="!py-1.5"
												key={item.label}
												onSelect={() => {
													if (item.link) {
														router.push(item.link); // Redirecione para a rota se existir
													}
													setOpen(false);
												}}
												style={
													item.disabled
														? {
																opacity: 0.5,
																pointerEvents:
																	"none",
															}
														: {}
												}
											>
												{item.icon && <item.icon />}
												<span>{item.label}</span>
												{/** biome-ignore lint/a11y/noStaticElementInteractions: <explanation> */}

												{/* {item.shortcut && <CommandShortcut>{item.shortcut}</CommandShortcut>} */}
											</CommandItem>
										))}
								</CommandGroup>
							</React.Fragment>
						),
					)}
				</CommandList>
			</CommandDialog>
		</>
	);
}
