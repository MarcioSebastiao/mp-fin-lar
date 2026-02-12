import { Route, Routes, BrowserRouter } from "react-router-dom";
import App from "../paginas/App";

/**
 * Componente responsável por configurar as rotas da aplicação.
 * A rota raiz leva à pagina App.
 */
export default function Rotas() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<App />} />
            </Routes>
        </BrowserRouter>
    );
}
