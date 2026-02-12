import axios from "axios";

/**
 * Instancia que centraliza o axios para comunicação com a API do backend
 * 
 * Configurações:
 * - baseURL: endereço da API do backend.
 * - headers: define Content-Type como application/json
 *
 */
const api = axios.create({
    baseURL: "http://localhost:5149",
    headers: {
        "Content-Type": "application/json"
    }
});

export default api;
