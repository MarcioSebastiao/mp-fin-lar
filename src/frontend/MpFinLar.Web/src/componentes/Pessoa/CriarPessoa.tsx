import { useState } from "react";
import { criarPessoa } from "../../servicos/pessoasServico";
import type { CriarPessoaDto } from "../../modelos/Pessoa";
import { extrairMensagensErro } from "../../servicos/api";
import type { ErroAPI } from "../../modelos/ErrosApi";
import { useForm } from "react-hook-form";

interface CriarPessoaProps {
    sucessoAoCriar: () => void;
}

function CriarPessoa({ sucessoAoCriar }: CriarPessoaProps) {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CriarPessoaDto>();

    const [mensagensErroApi, setErro] = useState<string[]>();

    async function cadrastrarPessoa(data: CriarPessoaDto) {
        try {
            await criarPessoa(data);
            alert("Pessoa cadastrada com sucesso!");
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
            <form onSubmit={handleSubmit(cadrastrarPessoa)}>
                <div className="item-formulario">
                    <label>Nome:</label>
                    <input
                        type="text"
                        {...register("nome", {
                            required: "Informe um nome para a pessoa",
                            maxLength: { value: 200, message: "O nome deve ter no máximo 200 caracteres." },
                        })}
                    />
                </div>
                <div className="item-formulario">
                    <label>Idade:</label>
                    <input
                        type="number"
                        min="0"
                        {...register("idade", {
                            required: "Informe uma idade para a pessoa.",
                        })}
                    />
                </div>
                <button type="submit">Cadastrar</button>
            </form>

            <div className="mensagens-erro">
                <span>{errors.nome?.message}</span>
                <span>{errors.idade?.message}</span>
                {mensagensErroApi?.map((item) => (
                    <span>{item}</span>
                ))}
            </div>
        </>
    );
}

export default CriarPessoa;
