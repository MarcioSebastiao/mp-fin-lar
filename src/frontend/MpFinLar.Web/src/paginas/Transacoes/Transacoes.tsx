import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import type { TransacaoResposta, ValoresTransacao } from "../../modelos/Transacao";
import { obterTransacoesDePessoa } from "../../servicos/transacoesServico";
import "./Transacoes.css";
import Modal from "../../componentes/Modal";
import FormTransacao from "../../componentes/Pessoa/FormTransacao";
import { formatarMoeda } from "../../utilitarios/formatadores";

function Transacoes() {
    const location = useLocation();
    const { pessoaId } = useParams();

    const [modalAberto, setModalAberto] = useState(false);
    const [transacoes, setTransacoes] = useState<TransacaoResposta[]>([]);
    const [valoresTransacao, setValoresTransacao] = useState<ValoresTransacao>({
        totalEmDespesas: 0,
        totalEmReceitas: 0,
        saldo: 0,
    });

    const [totalDeItens, setTotalDeItens] = useState(0);
    const [totalDeItensCarregados, setTotalDeItensCarregados] = useState(0);

    useEffect(() => {
        async function carregarTransacoes() {
            try {
                const resposta = await obterTransacoesDePessoa(pessoaId!);
                setTransacoes(resposta.transacoes);
                setValoresTransacao(resposta.valores);
                setTotalDeItens(resposta.totalDeItens);
                setTotalDeItensCarregados(resposta.transacoes.length);
            } catch (erro) {}
        }
        carregarTransacoes();
    }, []);

    function carregarMaisTransacoes() {
        async function carregar() {
            try {
                const resposta = await obterTransacoesDePessoa(pessoaId!, totalDeItensCarregados);
                setTransacoes((dados) => [...dados, ...resposta.transacoes]);
                setTotalDeItensCarregados((total) => total + resposta.transacoes.length);
            } catch (erro) {}
        }
        carregar();
    }

    function atualizarValores(transacaoResposta: TransacaoResposta) {
        if (transacaoResposta.tipo === "Despesa") {
            setValoresTransacao((valores) => ({
                ...valores,
                totalEmDespesas: valores.totalEmDespesas + transacaoResposta.valor,
                saldo: valores.saldo - transacaoResposta.valor,
            }));
        } else {
            setValoresTransacao((valores) => ({
                ...valores,
                totalEmReceitas: valores.totalEmReceitas + transacaoResposta.valor,
                saldo: valores.saldo + transacaoResposta.valor,
            }));
        }
    }

    return (
        <>
            <div className="container transacao">
                <h2>
                    <span>{location.state?.nomePessoa} </span> <span> - Transações</span>
                </h2>

                <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                    <FormTransacao
                        aoSucesso={async (novaTransacao) => {
                            setTransacoes((dados) => [novaTransacao, ...dados]);
                            atualizarValores(novaTransacao);
                            setModalAberto(false);
                        }}
                    />
                </Modal>

                <div className="abrir-modal">
                    <button className="botao" onClick={() => setModalAberto(true)}>
                        Nova Transação
                    </button>
                </div>

                <div className="valores-transacoes">
                    <p>
                        Total em Despesas:
                        <span> {formatarMoeda(valoresTransacao.totalEmDespesas)}</span>
                    </p>
                    <p>
                        Total em Receitas:
                        <span>{formatarMoeda(valoresTransacao.totalEmReceitas)}</span>
                    </p>
                    <p>
                        Saldo: <span className={valoresTransacao.saldo < 0 ? "negativo" : "positivo"}>{formatarMoeda(valoresTransacao.saldo)}</span>
                    </p>
                </div>

                <div className="lista-transacao">
                    <table>
                        <thead>
                            <tr>
                                <th>Descrição:</th>
                                <th>Categoria:</th>
                                <th>Tipo:</th>
                                <th>Valor:</th>
                            </tr>
                        </thead>
                        <tbody>
                            {transacoes.map((transacao) => (
                                <tr key={transacao.id}>
                                    <td>
                                        <span>{transacao.descricao}</span>
                                    </td>
                                    <td>
                                        <span>{transacao.categoria?.descricao}</span>
                                    </td>
                                    <td>
                                        <span className={transacao.tipo.toLocaleLowerCase()}>{transacao.tipo}</span>
                                    </td>
                                    <td>
                                        <span>{formatarMoeda(transacao.valor)}</span>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                    {totalDeItensCarregados < (totalDeItens ?? 0) && (
                        <div>
                            <button className="carregar-mais" onClick={carregarMaisTransacoes}>
                                Carregar Mais
                            </button>
                        </div>
                    )}
                </div>
            </div>
        </>
    );
}

export default Transacoes;
