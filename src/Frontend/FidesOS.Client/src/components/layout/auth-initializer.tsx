"use client";

import { useAuthStore } from "@/stores/auth-store";
import { useEffect } from "react";

export function AuthInitializer() {
  const { loadUser } = useAuthStore();

  useEffect(() => {
    loadUser();
  }, [loadUser]);

  return null;
}
