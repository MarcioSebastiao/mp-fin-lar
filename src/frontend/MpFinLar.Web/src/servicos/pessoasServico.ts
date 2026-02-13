import api from "./api";
import type { PessoaDto, Pessoa } from "../modelos/Pessoa";

export async function criarPessoa(dados: PessoaDto): Promise<Pessoa> {
    return (await api.post("/api/pessoa", dados)).data;
}

export async function atualizarPessoa(id: string, dados: PessoaDto): Promise<Pessoa> {
    return (await api.put(`/api/pessoa/${id}`, dados)).data;
}

export async function obterPessoas(): Promise<Pessoa[]> {
    const response = await api.get("/api/pessoa");
    return response.data as Pessoa[];
}

export async function removerPessoa(id: string): Promise<void> {
    await api.delete(`/api/pessoa/${id}`);
}