import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import type { ErroAPI } from "../../modelos/ErrosApi";
import { extrairMensagensErro } from "../../servicos/api";
import type { TransacaoDto, TransacaoResposta } from "../../modelos/Transacao";
import { criarTransacao } from "../../servicos/transacoesServico";
import type { Categoria } from "../../modelos/Categoria";
import { obterCategorias } from "../../servicos/categoriaServico";
import "./Forms.css";

type FormCategoriaProps = {
    aoSucesso: (categoria: TransacaoResposta) => void;
};

function FormCategoria({ aoSucesso }: FormCategoriaProps) {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TransacaoDto>({
        defaultValues: {
            descricao: "",
            valor: 0,
            tipo: 0,
            categoriaId: "",
            pessoaId: "",
        },
    });

    const { pessoaId } = useParams();
    const [mensagensErroApi, setErro] = useState<string[]>();
    const [categorias, setCategorias] = useState<Categoria[]>([]);
    const [categoriaId, setCategoriaId] = useState<string>("");

    const [totalDeCategorias, setTotalDeCategorias] = useState(0);
    const [totalDeCategoriasCarregadas, setTotalDeCategoriasCarregadas] = useState(0);

    async function enviar(transacao: TransacaoDto) {
        try {
            const novaTransacao = await criarTransacao({ ...transacao, pessoaId: pessoaId!, categoriaId: categoriaId });
            aoSucesso(novaTransacao);
        } catch (erro) {
            if ((erro as ErroAPI).status == 500) {
                alert("Erro inesperado. Por favor atualize e tente novamente!");
                return;
            }
            setErro(extrairMensagensErro(erro));
        }
    }

    useEffect(() => {
        async function carregarCategorias() {
            try {
                const resposta = await obterCategorias();
                setCategorias(resposta.categorias);
                setTotalDeCategorias(resposta.totalDeItens);
                setTotalDeCategoriasCarregadas(resposta.categorias.length);
            } catch (erro) {}
        }
        carregarCategorias();
    }, []);

    function carregarMaisCategorias() {
        async function carregar() {
            try {
                const resposta = await obterCategorias(totalDeCategoriasCarregadas);
                setCategorias((dados) => [...dados, ...resposta.categorias]);
                setTotalDeCategoriasCarregadas((total) => total + resposta.categorias.length);
            } catch (erro) {}
        }
        carregar();
    }

    return (
        <div className="form-transacao">
            <h2>Nova Transação</h2>
            <form onSubmit={handleSubmit(enviar)}>
                <div className="item-formulario">
                    <label>Descrição:</label>
                    <input
                        type="text"
                        {...register("descricao", {
                            required: "Informe uma descrição para a transação",
                            maxLength: { value: 400, message: "A descrição deve ter no máximo 400 caracteres." },
                        })}
                    />
                </div>

                <div className="item-formulario">
                    <label>Valor:</label>
                    <input
                        type="number"
                        {...register("valor", {
                            required: "Informe um valor para a transação",
                            min: { value: 0.01, message: "O valor deve ser maior que zero." },
                            valueAsNumber: true,
                        })}
                    />
                </div>

                <div className="item-formulario">
                    <label>Tipo:</label>
                    <select
                        id="tipo"
                        {...register("tipo", {
                            required: "Informe o tipo da transação.",
                            valueAsNumber: true,
                        })}
                    >
                        <option value="0">Selecione um tipo</option>
                        <option value="1">Despesa</option>
                        <option value="2">Receita</option>
                    </select>
                </div>

                <div className="item-formulario">
                    <fieldset>
                        <legend>Selecione uma categoria:</legend>
                        <div className="categorias">
                            {categorias.map((categoria) => (
                                <div className="item" onClick={() => setCategoriaId(categoria.id)}>
                                    <div>
                                        <label title={categoria.descricao} htmlFor={categoria.descricao}>
                                            {categoria.descricao}
                                        </label>
                                        <label htmlFor={categoria.finalidade}>{categoria.finalidade}</label>
                                    </div>
                                    <input type="radio" name="categoria" id={categoria.id} checked={categoriaId === categoria.id} />
                                </div>
                            ))}
                        </div>

                        {totalDeCategoriasCarregadas < (totalDeCategorias ?? 0) && (
                            <div>
                                <button type="button" className="carregar-mais" onClick={carregarMaisCategorias}>
                                    Carregar Mais
                                </button>
                            </div>
                        )}
                    </fieldset>
                </div>
                <button type="submit">Criar</button>
            </form>

            <div className="mensagens-erro">
                <span>{errors.descricao?.message}</span>
                <span>{errors.tipo?.message}</span>
                <span>{errors.valor?.message}</span>
                <span>{errors.categoriaId?.message}</span>
                <span>{errors.pessoaId?.message}</span>
                {mensagensErroApi?.map((item) => (
                    <span>{item}</span>
                ))}
            </div>
        </div>
    );
}

export default FormCategoria;
