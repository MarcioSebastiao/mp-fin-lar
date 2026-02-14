
export interface Categoria {
    id: string;
    descricao: string;
    finalidade: string;
}

export type CategoriaDto = {
    descricao: string;
    finalidade: number;
};
