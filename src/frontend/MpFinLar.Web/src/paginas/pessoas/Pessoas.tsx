import CriarPessoa from "../../componentes/Pessoa/CriarPessoa";
import Modal from "../../componentes/Modal";
import { useEffect, useState } from "react";
import "./Pessoas.css";
import { obterPessoas } from "../../servicos/pessoasServico";
import type { Pessoa } from "../../modelos/Pessoa";

function Pessoas() {
    const [modalAberto, setModalAberto] = useState(false);

    const [pessoas, setPessoas] = useState<Pessoa[]>([]);

    useEffect(() => {
        async function carregarPessoas() {
            try {
                setPessoas(await obterPessoas());
            } catch (erro) {}
        }
        carregarPessoas();
    }, []);

    return (
        <>
            <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                <CriarPessoa sucessoAoCriar={() => setModalAberto(false)} />
            </Modal>
            <div className="container">
                <div>
                    <button onClick={() => setModalAberto(true)}>Nova Pessoa</button>
                </div>

                <div className="lista-pessoas">
                    <table>
                        <thead>
                            <tr>
                                <th>Nome</th>
                                <th>Idade</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {pessoas.map((pessoa) => (
                                <tr>
                                    <td>{pessoa.nome}</td>
                                    <td>{pessoa.idade}</td>
                                    <td>
                                        <button className="editar">Editar</button>
                                        <button className="excluir">Excluir</button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </>
    );
}

export default Pessoas;
