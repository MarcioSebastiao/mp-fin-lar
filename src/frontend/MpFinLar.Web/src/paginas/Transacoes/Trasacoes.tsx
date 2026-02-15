import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import type { TransacaoResposta } from "../../modelos/Transacao";
import { obterTransacoesDePessoa } from "../../servicos/transacoesServico";
import "./Transacoes.css";
import Modal from "../../componentes/Modal";
import FormTransacao from "../../componentes/Pessoa/FormTransacao";

function Transacoes() {
    const location = useLocation();
    const { pessoaId } = useParams();

    const [modalAberto, setModalAberto] = useState(false);
    const [transacoes, setTransacoes] = useState<TransacaoResposta[]>([]);

    useEffect(() => {
        async function carregarTransacoes() {
            try {
                setTransacoes(await obterTransacoesDePessoa(pessoaId!, 0, 100));
            } catch (erro) {}
        }
        carregarTransacoes();
    }, []);

    return (
        <>
            <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                <FormTransacao
                    aoSucesso={async (novaTransacao) => {
                        setTransacoes((dados) => [novaTransacao, ...dados]);
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
