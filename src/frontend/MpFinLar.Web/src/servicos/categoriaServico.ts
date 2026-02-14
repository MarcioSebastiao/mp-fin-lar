import api from "./api";
import type { Categoria, CategoriaDto } from "../modelos/Categoria";

export async function criarCategoria(dados: CategoriaDto): Promise<Categoria> {
    return (await api.post("/api/categoria", dados)).data;
}

export async function obterCategorias(): Promise<Categoria[]> {
    const response = await api.get("/api/categoria");
    return response.data as Categoria[];
}