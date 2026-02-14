import { useEffect, useState } from "react";
import type { Categoria } from "../../modelos/Categoria";
import { obterCategorias } from "../../servicos/categoriaServico";
import Modal from "../../componentes/Modal";
import FormCategoria from "../../componentes/Pessoa/FormCategoria";
import "./Categorias.css";

function Categorias() {
    const [modalAberto, setModalAberto] = useState(false);
    const [categoria, setCategoria] = useState<Categoria[]>([]);

    useEffect(() => {
        async function carregarCategorias() {
            try {
                setCategoria(await obterCategorias());
            } catch (erro) {}
        }
        carregarCategorias();
    }, []);

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
                </div>
            </div>
        </>
    );
}

export default Categorias;
