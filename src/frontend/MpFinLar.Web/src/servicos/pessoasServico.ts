import api from "./api";
import type { CriarPessoaDto } from "../modelos/Pessoa";

export async function criarPessoa(dados: CriarPessoaDto): Promise<void> {
    await api.post("/api/pessoa", dados);
}
