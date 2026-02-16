import { Route, Routes, BrowserRouter, NavLink } from "react-router-dom";
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
                <h2>MpFinLar</h2>

                <div>
                    <NavLink to="/pessoas" className={({ isActive }) => (isActive ? "nav__link nav-link--ativo" : "nav-link")}>
                        Pessoas
                    </NavLink>
                    <NavLink to="/categorias" className={({ isActive }) => (isActive ? "nav__link nav-link--ativo" : "nav-link")}>
                        Categorias
                    </NavLink>
                </div>
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
