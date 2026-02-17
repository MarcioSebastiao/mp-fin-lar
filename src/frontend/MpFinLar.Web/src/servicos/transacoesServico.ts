import api from "./api";
import type { TransacaoDto, TransacaoResposta, TrasacoesResposta, ValoresTransacao } from "../modelos/Transacao";

export async function criarTransacao(dados: TransacaoDto): Promise<TransacaoResposta> {
    return (await api.post("/api/transacao", dados)).data;
}

export async function obterTransacoes(): Promise<TransacaoResposta[]> {
    const response = await api.get("/api/transacao");
    return response.data as TransacaoResposta[];
}

export async function obterTransacoesDePessoa(pessoaId: string, pularItens: number = 0, quantidadeItens: number = 100): Promise<TrasacoesResposta> {
    const response = await api.get(`/api/Transacao/${pessoaId}?pularItens=${pularItens}&quantidadeItens=${quantidadeItens}`);
    return response.data as TrasacoesResposta;
}

export async function obterValoresTransacoesPorCategoria(categoriaId: string): Promise<ValoresTransacao> {
    const response = await api.get(`/api/Transacao/${categoriaId}/valores-transacoes`);
    return response.data as ValoresTransacao;
}