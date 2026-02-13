export interface Pessoa {
    id: string;
    nome: string;
    idade: number;
}

export type PessoaDto = Omit<Pessoa, "id">;
