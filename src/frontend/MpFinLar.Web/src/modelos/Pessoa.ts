export interface Pessoa {
    id: number;
    nome: string;
    idade: number;
}

export type CriarPessoaDto = Omit<Pessoa, "id">;
