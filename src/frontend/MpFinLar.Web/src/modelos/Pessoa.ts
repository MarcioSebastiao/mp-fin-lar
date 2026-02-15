export interface Pessoa {
    id: string;
    nome: string;
    idade: number;
}

export type PessoaDto = Omit<Pessoa, "id">;

export type PessoasResposta = {
    pessoas: Pessoa[];
    totalDeItens: number;
};
