import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import type { TransacaoResposta, TrasacoesResposta, ValoresTransacao } from "../../modelos/Transacao";
import { obterTransacoesDePessoa } from "../../servicos/transacoesServico";
import "./Transacoes.css";
import Modal from "../../componentes/Modal";
import FormTransacao from "../../componentes/Pessoa/FormTransacao";

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

    useEffect(() => {
        async function carregarTransacoes() {
            try {
                const resposta = await obterTransacoesDePessoa(pessoaId!, 0, 100);
                setTransacoes(resposta.transacoes);
                setValoresTransacao(resposta.valores);
            } catch (erro) {}
        }
        carregarTransacoes();
    }, []);

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
            <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                <FormTransacao
                    aoSucesso={async (novaTransacao) => {
                        setTransacoes((dados) => [novaTransacao, ...dados]);
                        atualizarValores(novaTransacao);
                        setModalAberto(false);
                    }}
                />
            </Modal>
            <div>
                <h2>{location.state?.nomePessoa} - Transações:</h2>

                <div className="container transacao">
                    <div>
                        <button onClick={() => setModalAberto(true)}>Nova Transação</button>
                    </div>

                    <div className="valores">
                        <p>
                            Total em Despesas:
                            <span> {valoresTransacao.totalEmDespesas}</span>
                        </p>
                        <p>
                            Total em Receitas:
                            <span>{valoresTransacao.totalEmReceitas}</span>
                        </p>
                        <p>
                            Saldo: <span>{valoresTransacao.saldo}</span>
                        </p>
                    </div>

                    <div className="lista-transacao">
                        <table>
                            <thead>
                                <tr>
                                    <th>Descrição</th>
                                    <th>Tipo</th>
                                    <th>Valor</th>
                                </tr>
                            </thead>
                            <tbody>
                                {transacoes.map((transacoes) => (
                                    <tr key={transacoes.id}>
                                        <td>{transacoes.descricao}</td>
                                        <td>{transacoes.tipo}</td>
                                        <td>{transacoes.valor}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Transacoes;
