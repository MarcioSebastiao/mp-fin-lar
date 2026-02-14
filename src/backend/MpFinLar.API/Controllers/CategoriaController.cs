using Microsoft.AspNetCore.Mvc;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Aplicacao.Pessoas;

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
    public async Task<ActionResult<IEnumerable<PessoaRespostaDTO>>> Obter()
    {
        var pessoas = await _aplicacao.ObterAsync();
        return Ok(pessoas);
    }

}
