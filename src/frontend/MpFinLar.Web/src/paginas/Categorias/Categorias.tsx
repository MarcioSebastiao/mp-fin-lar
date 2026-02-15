import { useEffect, useState } from "react";
import type { Categoria } from "../../modelos/Categoria";
import { obterCategorias } from "../../servicos/categoriaServico";
import Modal from "../../componentes/Modal";
import FormCategoria from "../../componentes/Pessoa/FormCategoria";
import "./Categorias.css";

function Categorias() {
    const [modalAberto, setModalAberto] = useState(false);
    const [categoria, setCategoria] = useState<Categoria[]>([]);
    const [totalDeItens, setTotalDeItens] = useState(0);
    const [totalDeItensCarregados, setTotalDeItensCarregados] = useState(0);

    useEffect(() => {
        async function carregarCategorias() {
            try {
                const resposta = await obterCategorias();
                setCategoria(resposta.categorias);
                setTotalDeItens(resposta.totalDeItens);
                setTotalDeItensCarregados(resposta.categorias.length);
            } catch (erro) {}
        }
        carregarCategorias();
    }, []);

    function carregarMaisCategorias() {
        async function carregar() {
            try {
                const resposta = await obterCategorias(totalDeItensCarregados);
                setCategoria((dados) => [...dados, ...resposta.categorias]);
                setTotalDeItensCarregados((total) => total + resposta.categorias.length);
            } catch (erro) {}
        }
        carregar();
    }

    return (
        <>
            <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                <FormCategoria
                    aoSucesso={ async (novaCategoria) => {
                        setCategoria((dados) => [novaCategoria, ...dados]);
                        setModalAberto(false);
                    }}
                />
            </Modal>
            <div className="container">
                <div>
                    <button onClick={() => setModalAberto(true)}>Nova Categoria</button>
                </div>

                <div className="lista-categoria">
                    <table>
                        <thead>
                            <tr>
                                <th>Descrição</th>
                                <th>Finalidade</th>
                            </tr>
                        </thead>
                        <tbody>
                            {categoria.map((categoria) => (
                                <tr key={categoria.id}>
                                    <td>{categoria.descricao}</td>
                                    <td>{categoria.finalidade}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                    {totalDeItensCarregados < (totalDeItens ?? 0) && (
                        <div>
                            <button className="carregar-mais" onClick={carregarMaisCategorias}>
                                Carregar Mais
                            </button>
                        </div>
                    )}
                </div>
            </div>
        </>
    );
}

export default Categorias;
