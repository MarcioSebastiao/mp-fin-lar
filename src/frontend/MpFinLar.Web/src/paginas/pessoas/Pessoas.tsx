import Modal from "../../componentes/Modal";
import { useEffect, useState } from "react";
import "./Pessoas.css";
import { atualizarPessoa, criarPessoa, obterPessoas, removerPessoa as removePessoaService } from "../../servicos/pessoasServico";
import type { Pessoa } from "../../modelos/Pessoa";
import FormPessoa from "../../componentes/Pessoa/FormPessoa";
import { Link } from "react-router-dom";

function Pessoas() {
    const [modalAberto, setModalFormAberto] = useState(false);
    const [modalDeleteAberto, setModalDeleteAberto] = useState(false);
    const [pessoaSelecionada, setPessoaSelecionada] = useState<Pessoa | null>(null);
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [totalDeItens, setTotalDeItens] = useState(0);
    const [totalDeItensCarregados, setTotalDeItensCarregados] = useState(0);

    useEffect(() => {
        async function carregarPessoas() {
            try {
                const resposta = await obterPessoas();
                setPessoas(resposta.pessoas);
                setTotalDeItens(resposta.totalDeItens);
                setTotalDeItensCarregados(resposta.pessoas.length);
            } catch (erro) {}
        }
        carregarPessoas();
    }, []);

    function carregarMaisPessoas() {
        async function carregar() {
            try {
                const resposta = await obterPessoas(totalDeItensCarregados);
                setPessoas((dados) => [...dados, ...resposta.pessoas]);
                setTotalDeItensCarregados((total) => total + resposta.pessoas.length);
            } catch (erro) {}
        }
        carregar();
    }

    function abrirCriacao() {
        setPessoaSelecionada(null);
        setModalFormAberto(true);
    }

    function abrirEdicao(pessoa: Pessoa) {
        setPessoaSelecionada(pessoa);
        setModalFormAberto(true);
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
            <div className="container pessoas">
                <Modal aberto={modalAberto} aoFechar={() => setModalFormAberto(false)}>
                    <FormPessoa
                        pessoaInicial={pessoaSelecionada ?? undefined}
                        titulo={pessoaSelecionada ? "Editar Pessoa" : "Nova Pessoa"}
                        textoBotao={pessoaSelecionada ? "Salvar" : "Cadastrar"}
                        onSubmit={(pessoaDto) => (pessoaSelecionada ? atualizarPessoa(pessoaSelecionada.id, pessoaDto) : criarPessoa(pessoaDto))}
                        aoSucesso={async (novaPessoa) => {
                            pessoaSelecionada ? atualizarPessoaLista(novaPessoa) : adicionarPessoaLista(novaPessoa);
                            setModalFormAberto(false);
                        }}
                    />
                </Modal>

                <div className="abrir-modal">
                    <button className="botao" onClick={abrirCriacao}>
                        Adicionar Pessoa
                    </button>
                </div>

                <Modal aberto={modalDeleteAberto} aoFechar={() => setModalDeleteAberto(false)}>
                    <div className="remover-pessoa">
                        <p>Tem certeza que deseja remover essa pessoa?</p>
                        <div>
                            <span>{pessoaSelecionada?.nome}</span>
                            <span>{pessoaSelecionada?.idade} anos</span>
                        </div>
                        <div>
                            <span
                                onClick={async () => {
                                    removerPessoa(pessoaSelecionada!.id)
                                    setModalDeleteAberto(false);
                                }}
                            >
                                Sim!
                            </span>
                            <button>Não!</button>
                        </div>
                    </div>
                </Modal>

                <div className="lista-pessoas">
                    <table>
                        <thead>
                            <tr>
                                <th>Nome:</th>
                                <th>Idade:</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {pessoas.map((pessoa) => (
                                <tr key={pessoa.id}>
                                    <td>
                                        <span>{pessoa.nome}</span>
                                    </td>
                                    <td>
                                        <span>{pessoa.idade} anos</span>
                                    </td>
                                    <td>
                                        <div>
                                            <Link to={`${pessoa.id}/transacoes`} state={{ nomePessoa: pessoa.nome }}>
                                                Transações
                                            </Link>
                                            <button className="editar" onClick={() => abrirEdicao(pessoa)}>
                                                Editar
                                            </button>
                                            <button
                                                className="remover"
                                                onClick={() => {
                                                    setPessoaSelecionada(pessoa);
                                                    setModalDeleteAberto(true);
                                                }}
                                            >
                                                Remover
                                            </button>
                                        </div>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                    {totalDeItensCarregados < (totalDeItens ?? 0) && (
                        <div>
                            <button className="carregar-mais" onClick={carregarMaisPessoas}>
                                Carregar Mais
                            </button>
                        </div>
                    )}
                </div>
            </div>
        </>
    );
}

export default Pessoas;
