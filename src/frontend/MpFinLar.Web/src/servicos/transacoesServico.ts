import api from "./api";
import type { TransacaoDto, TransacaoResposta } from "../modelos/Transacao";

export async function criarTransacao(dados: TransacaoDto): Promise<TransacaoResposta> {
    return (await api.post("/api/transacao", dados)).data;
}

export async function obterTransacoes(): Promise<TransacaoResposta[]> {
    const response = await api.get("/api/transacao");
    return response.data as TransacaoResposta[];
}

export async function obterTransacoesDePessoa(pessoaId: string, pularItens: number, quantidadeItens: number): Promise<TransacaoResposta[]> {
    const response = await api.get(`/api/Transacao/${pessoaId}?pularItens=${pularItens}&quantidadeItens=${quantidadeItens}`);
    return response.data as TransacaoResposta[];
}
