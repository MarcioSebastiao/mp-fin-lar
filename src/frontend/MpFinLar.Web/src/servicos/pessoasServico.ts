import api from "./api";
import type { PessoaDto, Pessoa, PessoasResposta } from "../modelos/Pessoa";

export async function criarPessoa(dados: PessoaDto): Promise<Pessoa> {
    return (await api.post("/api/pessoa", dados)).data;
}

export async function atualizarPessoa(id: string, dados: PessoaDto): Promise<Pessoa> {
    return (await api.put(`/api/pessoa/${id}`, dados)).data;
}

export async function obterPessoas(pularItens: number = 0, quantidadeItens: number = 100): Promise<PessoasResposta> {
    const response = await api.get(`/api/pessoa?pularItens=${pularItens}&quantidadeItens=${quantidadeItens}`);
    return response.data as PessoasResposta;
}

export async function removerPessoa(id: string): Promise<void> {
    await api.delete(`/api/pessoa/${id}`);
}