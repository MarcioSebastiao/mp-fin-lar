import { useState } from "react";
import type { Pessoa, PessoaDto } from "../../modelos/Pessoa";
import { extrairMensagensErro } from "../../servicos/api";
import type { ErroAPI } from "../../modelos/ErrosApi";
import { useForm } from "react-hook-form";

interface FormPessoaProps {
    pessoaInicial?: PessoaDto;
    titulo: string;
    textoBotao: string;
    onSubmit: (pessoa: PessoaDto) => Promise<Pessoa>;
    aoSucesso: (pessoa: Pessoa) => void;
}

function FormPessoa({ pessoaInicial, titulo, textoBotao, onSubmit, aoSucesso }: FormPessoaProps) {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<PessoaDto>({
        defaultValues: pessoaInicial,
    });

    const [mensagensErroApi, setErro] = useState<string[]>();

    async function enviar(data: PessoaDto) {
        try {
            const novaPessoa = await onSubmit(data);
            aoSucesso(novaPessoa);
        } catch (erro) {
            if ((erro as ErroAPI).status == 500) {
                alert("Erro inesperado. Por favor atualize e tente novamente!");
                return;
            }
            setErro(extrairMensagensErro(erro));
        }
    }

    return (
        <div>
            <h2>{titulo}</h2>
            <form onSubmit={handleSubmit(enviar)}>
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
                <button type="submit">{textoBotao}</button>
            </form>

            <div className="mensagens-erro">
                <span>{errors.nome?.message}</span>
                <span>{errors.idade?.message}</span>
                {mensagensErroApi?.map((item) => (
                    <span>{item}</span>
                ))}
            </div>
        </div>
    );
}

export default FormPessoa;
