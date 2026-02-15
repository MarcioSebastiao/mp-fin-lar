import Modal from "../../componentes/Modal";
import { useEffect, useState } from "react";
import "./Pessoas.css";
import { atualizarPessoa, criarPessoa, obterPessoas, removerPessoa as removePessoaService } from "../../servicos/pessoasServico";
import type { Pessoa, PessoaDto } from "../../modelos/Pessoa";
import FormPessoa from "../../componentes/Pessoa/FormPessoa";
import { Link } from "react-router-dom";

function Pessoas() {
    const [modalAberto, setModalAberto] = useState(false);
    const [pessoaSelecionada, setPessoaSelecionada] = useState<Pessoa | null>(null);
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);

    useEffect(() => {
        async function carregarPessoas() {
            try {
                setPessoas(await obterPessoas());
            } catch (erro) {}
        }
        carregarPessoas();
    }, []);

    function abrirCriacao() {
        setPessoaSelecionada(null);
        setModalAberto(true);
    }

    function abrirEdicao(pessoa: Pessoa) {
        setPessoaSelecionada(pessoa);
        setModalAberto(true);
    }

    function atualizarPessoaLista(pessoa: Pessoa) {
        setPessoas((prev) => prev.map((p) => (p.id === pessoa.id ? pessoa : p)));
    }

    function adicionarPessoaLista(novaPessoa: Pessoa) {
        setPessoas((prev) => [novaPessoa, ...prev]);
    }

    async function removerPessoa(id: string) {
        try {
            await removePessoaService(id);
            setPessoas((pe) => pe.filter((p) => p.id !== id));
        } catch (erro) {}
    }

    return (
        <>
            <Modal aberto={modalAberto} aoFechar={() => setModalAberto(false)}>
                <FormPessoa
                    pessoaInicial={pessoaSelecionada ?? undefined}
                    titulo={pessoaSelecionada ? "Editar Pessoa" : "Nova Pessoa"}
                    textoBotao={pessoaSelecionada ? "Salvar" : "Cadastrar"}
                    onSubmit={(pessoaDto) => (pessoaSelecionada ? atualizarPessoa(pessoaSelecionada.id, pessoaDto) : criarPessoa(pessoaDto))}
                    aoSucesso={async (novaPessoa) => {
                        pessoaSelecionada ? atualizarPessoaLista(novaPessoa) : adicionarPessoaLista(novaPessoa);
                        setModalAberto(false);
                    }}
                />
            </Modal>
            <div className="container">
                <div>
                    <button onClick={abrirCriacao}>Nova Pessoa</button>
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
                                <tr key={pessoa.id}>
                                    <td>{pessoa.nome}</td>
                                    <td>{pessoa.idade}</td>
                                    <td>
                                        <Link to={`${pessoa.id}/transacoes`} state={{ nomePessoa: pessoa.nome }}>
                                            Transações
                                        </Link>
                                        <button className="editar" onClick={() => abrirEdicao(pessoa)}>
                                            Editar
                                        </button>
                                        <button className="excluir" onClick={async () => removerPessoa(pessoa.id)}>
                                            Excluir
                                        </button>
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
