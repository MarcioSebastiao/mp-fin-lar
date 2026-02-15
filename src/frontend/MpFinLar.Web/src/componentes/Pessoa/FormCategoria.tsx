import { useState } from "react";
import type { Categoria, CategoriaDto } from "../../modelos/Categoria";
import { criarCategoria } from "../../servicos/categoriaServico";
import { useForm } from "react-hook-form";
import type { ErroAPI } from "../../modelos/ErrosApi";
import { extrairMensagensErro } from "../../servicos/api";

type FormCategoriaProps = {
    aoSucesso: (categoria: Categoria) => void;
};

function FormCategoria({ aoSucesso }: FormCategoriaProps) {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CategoriaDto>({
        defaultValues: {
            descricao: "",
            finalidade: 0,
        },
    });

    const [mensagensErroApi, setErro] = useState<string[]>();

    async function enviar(categoria: CategoriaDto) {
        try {
            const novaCategoria = await criarCategoria(categoria);
            aoSucesso(novaCategoria);
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
            <h2>Nova Categoria</h2>
            <form onSubmit={handleSubmit(enviar)}>
                <div className="item-formulario">
                    <label>Descrição:</label>
                    <input
                        type="text"
                        {...register("descricao", {
                            required: "Informe uma descrição para a categoria",
                            maxLength: { value: 400, message: "A descrição deve ter no máximo 400 caracteres." },
                        })}
                    />
                </div>
                <div className="item-formulario">
                    <label>Finalidade:</label>
                    <select
                        id="finalidade"
                        {...register("finalidade", {
                            required: "Informe uma finalidade para a categoria.",
                            valueAsNumber: true,
                        })}
                    >
                        <option value="0">Selecione uma finalidade</option>
                        <option value="1">Despesa</option>
                        <option value="2">Receita</option>
                        <option value="3">Ambas</option>
                    </select>
                </div>
                <button type="submit">Criar</button>
            </form>

            <div className="mensagens-erro">
                <span>{errors.descricao?.message}</span>
                <span>{errors.finalidade?.message}</span>
                {mensagensErroApi?.map((item) => (
                    <span>{item}</span>
                ))}
            </div>
        </div>
    );
}

export default FormCategoria;
