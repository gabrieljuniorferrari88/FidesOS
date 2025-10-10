import { AccountSwitcher } from "./account-switcher";

export default function Header() {
	return (
		<header className="flex h-14 items-center gap-4 border-b bg-muted/40 px-4 lg:h-[60px] lg:px-6">
			<div className="w-full flex-1">
				{/* Espaço para busca ou título da página no futuro */}
			</div>
			<AccountSwitcher />
		</header>
	);
}
