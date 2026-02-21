using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MpFinLar.API.Aplicacao;
using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Constantes;

namespace MpFinLar.API.Controllers;

public sealed class CategoriaController : MainController
{
    private readonly ICategoriaAplicacao _aplicacao;
    private readonly IMemoryCache _cache;

    public CategoriaController(ICategoriaAplicacao aplicacao, IMemoryCache cache)
    {
        _aplicacao = aplicacao;
        _cache = cache;
    }

    [HttpPost]
    public async Task<ActionResult> Criar(CategoriaDto categoriaDto)
    {
        (CategoriaRespostaDTO? categoria, ResultadoAplicacao resultado) = await _aplicacao.CriarAsync(categoriaDto);

        if (!resultado.Sucesso)
            return RespostaDeErro(resultado.Notificacoes);

        _cache.Remove(CacheConstantes.CategoriasRespostaCacheKey);

        return Ok(categoria);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remover(Guid id)
    {
        var resultado = await _aplicacao.RemoverAsync(id);
        if (!resultado.Sucesso)
            return RespostaDeErro(resultado.Notificacoes);

        _cache.Remove(CacheConstantes.CategoriasRespostaCacheKey);
        return Ok();
    }


    [HttpGet]
    public async Task<ActionResult<CategoriasRespostaDTO>> Obter(int pularItens, int quantidadeItens)
    {
        if (_cache.TryGetValue(CacheConstantes.CategoriasRespostaCacheKey, out CategoriasRespostaDTO? categorias))
            return categorias!;

        categorias = await _aplicacao.ObterAsync(pularItens, quantidadeItens);

        _cache.Set(CacheConstantes.CategoriasRespostaCacheKey, categorias, TimeSpan.FromMinutes(10));

        return Ok(categorias);
    }

}
