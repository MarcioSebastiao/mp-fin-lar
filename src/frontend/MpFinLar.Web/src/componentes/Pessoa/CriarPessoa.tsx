import React, { useState } from "react";
import { criarPessoa } from "../../servicos/pessoasServico";
import type { CriarPessoaDto } from "../../modelos/Pessoa";
import { extrairMensagensErro } from "../../servicos/api";
import type { ErroAPI } from "../../modelos/ErrosApi";

interface CriarPessoaProps {
    sucessoAoCriar: () => void;
}

function CriarPessoa({ sucessoAoCriar }: CriarPessoaProps) {
    const [pessoa, setPessoa] = useState<CriarPessoaDto>({
        nome: "",
        idade: 0,
    });

    const [mensagensErro, setErro] = useState<string[]>();

    function manipularMudancaNome(evento: React.ChangeEvent<HTMLInputElement>) {
        setPessoa({ ...pessoa, nome: evento.target.value });
    }

    function manipularMudancaIdade(evento: React.ChangeEvent<HTMLInputElement>) {
        setPessoa({ ...pessoa, idade: Number(evento.target.value) });
    }

    function limparFormulario() {
        const modeloInicial: CriarPessoaDto = {
            nome: "",
            idade: 0,
        };
        setPessoa(modeloInicial);
    }

    async function cadrastrarPessoa(evento: React.SyntheticEvent<HTMLFormElement>) {
        evento.preventDefault();
        try {
            await criarPessoa(pessoa);
            alert("Pessoa cadastrada com sucesso!");
            limparFormulario();
            sucessoAoCriar();
        } catch (erro) {
            if ((erro as ErroAPI).status == 500) {
                alert("Erro inesperado. Por favor atualize e tente novamente!");
                return;
            }
            setErro(extrairMensagensErro(erro));
        }
    }

    return (
        <>
            <h2>Nova Pessoa</h2>
            <form onSubmit={cadrastrarPessoa}>
                <div className="item-formulario">
                    <label>Nome:</label>
                    <input type="text" onChange={manipularMudancaNome} value={pessoa.nome} />
                </div>
                <div className="item-formulario">
                    <label>Idade:</label>
                    <input type="number" onChange={manipularMudancaIdade} value={pessoa.idade} />
                </div>
                <button type="submit">Cadastrar</button>
            </form>

            <div className="mensagens-erro">
                {mensagensErro?.map((item) => (
                    <span>{item}</span>
                ))}
            </div>
        </>
    );
}

export default CriarPessoa;
