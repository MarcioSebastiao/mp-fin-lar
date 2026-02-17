import type { Categoria } from "./Categoria";
import type { Pessoa } from "./Pessoa";

export interface TransacaoDto {
    descricao: string;
    valor: number;
    tipo: number;
    categoriaId: string;
    pessoaId: string;
}

export interface TransacaoResposta {
    id: string;
    descricao: string;
    valor: number;
    tipo: string;
    categoria: Categoria;
    pessoa: Pessoa;
}

export interface ValoresTransacao {
    totalEmDespesas: number;
    totalEmReceitas: number;
    saldo: number;
}

export type TrasacoesResposta = {
    transacoes: TransacaoResposta[];
    valores: ValoresTransacao;
    totalDeItens: number;
};
