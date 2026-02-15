import { Route, Routes, BrowserRouter, Link } from "react-router-dom";
import Pessoas from "../paginas/pessoas/Pessoas";
import Categorias from "../paginas/Categorias/Categorias";
import Transacoes from "../paginas/Transacoes/Transacoes";

/**
 * Componente responsável por configurar as rotas da aplicação.
 * A rota raiz leva à pagina App.
 */
export default function Rotas() {
    return (
        <BrowserRouter>
            <nav>
                <Link to="/pessoas">Pessoas</Link>
                <Link to="/categorias">Categorias</Link>
            </nav>
            
            <Routes>
                <Route path="/" element={<Pessoas />} />
                <Route path="/pessoas" element={<Pessoas />} />
                <Route path="/categorias" element={<Categorias />} />
                <Route path="/pessoas/:pessoaId/transacoes" element={<Transacoes />} />
            </Routes>
        </BrowserRouter>
    );
}
