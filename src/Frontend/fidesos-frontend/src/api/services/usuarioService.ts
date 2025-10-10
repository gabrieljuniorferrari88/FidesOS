import { RespostaUsuarioPerfilJson } from "@/api/contracts/responses";
import api from "..";

export const getProfile = async () => {
    const response = await api.get<RespostaUsuarioPerfilJson>('/usuario');
    return response.data;
}
