import api from "./api";
import type { Categoria, CategoriaDto, CategoriasResposta } from "../modelos/Categoria";

export async function criarCategoria(dados: CategoriaDto): Promise<Categoria> {
    return (await api.post("/api/categoria", dados)).data;
}

export async function obterCategorias(pularItens: number = 0, quantidadeItens: number = 100): Promise<CategoriasResposta> {
    const response = await api.get(`/api/categoria?pularItens=${pularItens}&quantidadeItens=${quantidadeItens}`);
    return response.data as CategoriasResposta;
}

export async function removerCategoria(id: string): Promise<void> {
    await api.delete(`/api/categoria/${id}`);
}