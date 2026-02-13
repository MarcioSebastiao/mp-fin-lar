export interface Pessoa {
    id: string;
    nome: string;
    idade: number;
}

export type CriarPessoaDto = Omit<Pessoa, "id">;
