import api from "./api";
import type { CriarPessoaDto, Pessoa } from "../modelos/Pessoa";

export async function criarPessoa(dados: CriarPessoaDto): Promise<void> {
    await api.post("/api/pessoa", dados);
}

export async function obterPessoas(): Promise<Pessoa[]> {
    const response = await api.get("/api/pessoa");
    return response.data as Pessoa[];
}
