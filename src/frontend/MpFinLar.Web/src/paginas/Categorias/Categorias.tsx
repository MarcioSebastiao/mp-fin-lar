import { useEffect, useState } from "react";
import type { Categoria } from "../../modelos/Categoria";
import { obterCategorias } from "../../servicos/categoriaServico";
import Modal from "../../componentes/Modal";
import FormCategoria from "../../componentes/Pessoa/FormCategoria";
import "./Categorias.css";
import type { ValoresTransacao } from "../../modelos/Transacao";
import { formatarMoeda } from "../../utilitarios/formatadores";
import { obterValoresTransacoesPorCategoria } from "../../servicos/transacoesServico";

function Categorias() {
    const [modalFormAberto, setModalFormAberto] = useState(false);
    const [modalValoresAberto, setModalValoresAberto] = useState(false);
    const [categoria, setCategoria] = useState<Categoria[]>([]);
    const [totalDeItens, setTotalDeItens] = useState(0);
    const [totalDeItensCarregados, setTotalDeItensCarregados] = useState(0);
    const [valoresCategoria, setValoresCategoria] = useState<{ DescricaoCategoria: string; ValoresTransacao: ValoresTransacao }>({
        DescricaoCategoria: "",
        ValoresTransacao: {
            totalEmDespesas: 0,
            totalEmReceitas: 0,
            saldo: 0,
        },
    });

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
            <Modal aberto={modalFormAberto} aoFechar={() => setModalFormAberto(false)}>
                <FormCategoria
                    aoSucesso={async (novaCategoria) => {
                        setCategoria((dados) => [novaCategoria, ...dados]);
                        setModalFormAberto(false);
                    }}
                />
            </Modal>
            <div className="abrir-modal">
                <button className="botao" onClick={() => setModalFormAberto(true)}>
                    Nova Categoria
                </button>
            </div>

            <Modal aberto={modalValoresAberto} aoFechar={() => setModalValoresAberto(false)}>
                <div className="modal-valores-trasacoes">
                    <span>{valoresCategoria.DescricaoCategoria}</span>
                    <div className="valores-transacoes">
                        <p>
                            Total em Despesas:
                            <span> {formatarMoeda(valoresCategoria.ValoresTransacao.totalEmDespesas)}</span>
                        </p>
                        <p>
                            Total em Receitas:
                            <span>{formatarMoeda(valoresCategoria.ValoresTransacao.totalEmReceitas)}</span>
                        </p>
                        <p>
                            Saldo:{" "}
                            <span className={valoresCategoria.ValoresTransacao.saldo < 0 ? "negativo" : "positivo"}>
                                {formatarMoeda(valoresCategoria.ValoresTransacao.saldo)}
                            </span>
                        </p>
                    </div>
                </div>
            </Modal>
            <div className="container">
                <div className="lista-categoria">
                    <table>
                        <thead>
                            <tr>
                                <th>Descrição:</th>
                                <th>Finalidade:</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {categoria.map((categoria) => (
                                <tr key={categoria.id}>
                                    <td>
                                        <span>{categoria.descricao}</span>
                                    </td>
                                    <td>
                                        <span className={categoria.finalidade.toLocaleLowerCase()}>{categoria.finalidade}</span>
                                    </td>
                                    <td>
                                        <span
                                            onClick={async () => {
                                                const valores = await obterValoresTransacoesPorCategoria(categoria.id);
                                                setValoresCategoria({ DescricaoCategoria: categoria.descricao, ValoresTransacao: valores });
                                                setModalValoresAberto(true);
                                            }}
                                        >
                                            Consultar Valores
                                        </span>
                                    </td>
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
