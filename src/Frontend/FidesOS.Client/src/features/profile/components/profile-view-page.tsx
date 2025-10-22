"use client";

import { useAuthStore } from "@/stores/auth-store";
import { usePathname } from "next/navigation";
import ChangePasswordForm from "./change-password-form";
import { ProfileSidebarNav } from "./profile-sidebar-nav";
import UpdateProfileForm from "./update-profile-form";

export default function ProfileViewPage() {
  const pathname = usePathname();
  const { isLoading } = useAuthStore();

  // Decide qual componente de formulário renderizar
  const ActiveForm = () => {
    if (pathname === "/dashboard/profile/security") {
      return <ChangePasswordForm />;
    }
    return <UpdateProfileForm />;
  };

  if (isLoading) {
    return <div className="p-4">Carregando perfil...</div>;
  }

  return (
    <div className="flex w-full flex-col p-4 md:p-6 space-y-6">
      <header>
        <h2 className="text-2xl font-bold tracking-tight">Account</h2>
        <p className="text-muted-foreground">Manage your account info.</p>
      </header>

      <div className="flex flex-col space-y-8 lg:flex-row lg:space-x-12 lg:space-y-0">
        <aside className="lg:w-1/5">
          <ProfileSidebarNav />
        </aside>
        <div className="flex-1 lg:max-w-2xl">
          <ActiveForm />
        </div>
      </div>
    </div>
  );
}
