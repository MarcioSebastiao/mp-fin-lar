import axios from "axios";
import type { ErroValidacao } from "../modelos/ErrosApi";

/**
 * Instância que centraliza o axios para comunicação com a API do backend
 *
 * Configurações:
 * - baseURL: endereço da API do backend.
 * - headers: define Content-Type como application/json
 *
 */
const api = axios.create({
    baseURL: "http://localhost:5149",
    headers: { "Content-Type": "application/json" },
});

/**
 * Interceptador global de respostas da API.
 * Permite padronizar o tratamento de erros antes que cheguem aos componentes.
 * 
 */
api.interceptors.response.use(
    (resposta) => resposta,
    // Caso ocorra erro (4xx ou 5xx), será tratado aqui.
    (erro) => {
        // Verifica se é um erro de validação que retorna 400 e se possui a propriedade "errors"
        if (erro.response?.status == 400 && erro.response?.data?.errors) {
            const erroValidacao: ErroValidacao = erro.response.data;
             // Promise.reject propaga o erro para o bloco catch de quem chamou o serviço.
            return Promise.reject(erroValidacao);
        }
        // Caso não seja erro de validação, retorna um erro genérico padronizado.
        return Promise.reject({
            titulo: "Erro inesperado",
            status: erro.response?.status ?? 500,
        });
    },
);

/**
 * Extrai todas as mensagens de erro;
 * 
 * O backend retorna os erros no formato:
 * {
 *   errors: {
 *     Campo1: ["Mensagem 1"],
 *     Campo2: ["Mensagem 2"]
 *   }
 * }
 *
 * Essa função transforma esse objeto em um array simples de mensagens:
 * ["Mensagem 1", "Mensagem 2"]
 */
export function extrairMensagensErro(erro: unknown): string[] {
    const errors: Record<string, string[]> = (erro as ErroValidacao).errors;
    return Object.values(errors).flat();
}

export default api;
