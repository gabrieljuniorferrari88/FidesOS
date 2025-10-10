import {
	BookA,
	Calendar,
	ChartLine,
	ChartPie,
	Fingerprint,
	Forklift,
	Grid2X2,
	Home,
	Kanban,
	Lock,
	type LucideIcon,
	Mail,
	MessageSquare,
	ReceiptText,
	ShoppingBag,
	Users
} from 'lucide-react';

export interface NavSubItem {
	title: string;
	url: string;
	icon?: LucideIcon;
	comingSoon?: boolean;
	newTab?: boolean;
}

export interface NavMainItem {
	title: string;
	url: string;
	icon?: LucideIcon;
	subItems?: NavSubItem[];
	comingSoon?: boolean;
	newTab?: boolean;
}

export interface NavGroup {
	id: number;
	label?: string;
	items: NavMainItem[];
}

export const sidebarItems: NavGroup[] = [
	{
		id: 1,
		label: 'Dashboards',
		items: [
			{
				title: 'Dashboards',
				url: '/dashboard',
				icon: Home,
				subItems: [
					{ title: 'Default', url: '/dashboard/default', icon: ChartPie },
					{ title: 'About', url: '/dashboard/about', icon: Grid2X2 },
					{ title: 'CRM', url: '/dashboard', icon: Grid2X2, comingSoon: true },
					{
						title: 'Analytics',
						url: '/dashboard/analytics',
						icon: ChartLine,
						comingSoon: true,
					},
					{
						title: 'eCommerce',
						url: '/dashboard/e-commerce',
						icon: ShoppingBag,
						comingSoon: true,
					},
					{ title: 'Academy', url: '/dashboard/academy', icon: BookA, comingSoon: true },
					{
						title: 'Logistics',
						url: '/dashboard/logistics',
						icon: Forklift,
						comingSoon: true,
					},
				],
			},
		],
	},
	{
		id: 2,
		label: 'Pages',
		items: [
			{
				title: 'Authentication',
				url: '/auth',
				icon: Fingerprint,
				// subItems: [
				// 	{ title: 'Login v1', url: '/auth/login', newTab: true },
				// 	{ title: 'Register v1', url: '/auth/register', newTab: true },
				// ],
			},
			{
				title: 'Email',
				url: '/mail',
				icon: Mail,
				comingSoon: true,
			},
			{
				title: 'Chat',
				url: '/chat',
				icon: MessageSquare,
				comingSoon: true,
			},
			{
				title: 'Calendar',
				url: '/calendar',
				icon: Calendar,
				comingSoon: true,
			},
			{
				title: 'Kanban',
				url: '/kanban',
				icon: Kanban,
				comingSoon: true,
			},
			{
				title: 'Invoice',
				url: '/invoice',
				icon: ReceiptText,
				comingSoon: true,
			},
			{
				title: 'Users',
				url: '/users',
				icon: Users,
				comingSoon: true,
			},
			{
				title: 'Roles',
				url: '/roles',
				icon: Lock,
				comingSoon: true,
			},
		],
	}
];
