using Microsoft.AspNetCore.Mvc;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Categorias;

namespace MpFinLar.API.Controllers;

public sealed class CategoriaController : MainController
{
    private readonly ICategoriaAplicacao _aplicacao;
    public CategoriaController(ICategoriaAplicacao aplicacao)
    {
        _aplicacao = aplicacao;
    }

    [HttpPost]
    public async Task<ActionResult> Criar(CategoriaDto categoriaDto)
    {
        (CategoriaRespostaDTO? categoria, ResultadoAplicacao resultado) = await _aplicacao.CriarAsync(categoriaDto);

        if (resultado.Sucesso)
            return Ok(categoria);

        return RespostaDeErro(resultado.Notificacoes);
    }



    [HttpGet]
    public async Task<ActionResult<CategoriasRespostaDTO>> Obter(int pularItens, int quantidadeItens)
    {
        var categorias = await _aplicacao.ObterAsync(pularItens, quantidadeItens);
        return Ok(categorias);
    }

}
